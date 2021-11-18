using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using DbAccess.Data;
using ImmoNet_Api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DTO;
using Serilog;

namespace ImmoNet_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<Contact> _signInManager;
        private readonly UserManager<Contact> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _aPISettings;

        public AccountController(SignInManager<Contact> signInManager,
                                    UserManager<Contact> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                            IOptions<APISettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _aPISettings = options.Value;
        }

        [HttpPost(Name = "SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || !ModelState.IsValid)
            {
                Log.Information("Invalid sign up.");
                return BadRequest();
            }

            var user = new Contact
            {
                UserName = userRequestDTO.Email,
                Email = userRequestDTO.Email,
                ContactName = userRequestDTO.Name,
                PhoneNumber = userRequestDTO.Phone,
                EmailConfirmed = true,
                CreatedOn = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, userRequestDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                Log.Error("The registration was unsuccessful.");
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }
            var roleResult = await _userManager.AddToRoleAsync(user, RoleDefinition.Role_User);
            if (!roleResult.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                Log.Error("The registration was unsuccessful.");
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }
            Log.Information("Successful sign up!");
            return StatusCode(201);
        }

        [HttpPost(Name = "SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
        {

            var result = await _signInManager.PasswordSignInAsync(authenticationDTO.Email, authenticationDTO.Password, false, false);
            if (result.Succeeded)
            {
                var contact = await _userManager.FindByEmailAsync(authenticationDTO.Email);
                if (contact == null)
                {
                    Log.Error("Invalid authentication.");
                    return Unauthorized(new AuthenticationResponseDTO
                    {
                        IsAuthenticationSuccessful = false,
                        ErrorMessage = "Invalid Authentication!"
                    });
                }

                //everything is valid and we need to login the user.

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(contact);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(90),
                    signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                Log.Information("User successfully signed in!");
                return Ok(new AuthenticationResponseDTO
                {
                    IsAuthenticationSuccessful = true,
                    Token = token,
                    contactDTO = new ContactDTO
                    {
                        Id = contact.Id,
                        ContactName = contact.ContactName,
                        Email = contact.Email,
                        PhoneNumber = contact.PhoneNumber,
                    }
                });
            }
            else
            {
                Log.Error("Invalid authentication.");
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthenticationSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(Contact contact)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, contact.Email),
                new Claim(ClaimTypes.Email, contact.Email),
                new Claim("Id", contact.Id),
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(contact.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}