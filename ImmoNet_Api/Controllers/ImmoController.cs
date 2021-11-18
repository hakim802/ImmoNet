using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using DataContext.Repository.IRepository;
using DataContext.UnitOfWorkPattern;
using DataContext.UnitOfWorkPattern.IUnitOfWorkPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using Serilog;

namespace ImmoNet_Api.Controllers
{
    [Route("api/[controller]")]
    public class ImmoController : Controller
    {
        private readonly IImmoRepository _immoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImmoController(IImmoRepository immoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _immoRepository = immoRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ImmoDTO immoDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _immoRepository.CreateImmo(immoDTO);
                return Ok(immoDTO);
            }
            return Ok(immoDTO);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllImmos()
        {
            try
            {
                var allImmos = await _unitOfWork.ImmoRepository.GetAll();
                return Ok(allImmos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Something went wrong in the {nameof(GetAllImmos)}");
                return StatusCode(500);
            }
        }

        [HttpGet("Province")]
        public async Task<IActionResult> GetImmoByProvince(string province)
        {
            try
            {
                var immos = await _unitOfWork.ImmoRepository.Get(x => x.Province == province);
                //null, new List<string> { "Immos" });
                //new List<string> { "Images" });
                //var result = _mapper.Map<IList<ImmoDTO>>(immos);
                return Ok(immos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Something went wrong in the {nameof(GetImmoByProvince)}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immo = await _unitOfWork.ImmoRepository.Get
                (m => m.ImmoId == id);
            if (immo == null)
            {
                return NotFound();
            }

            return Ok(immo);
        }

        // POST: Immo/Delete/5
        [HttpDelete(Name = "Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var immo = await _immoRepository.DeleteImmo(id);
            await _unitOfWork.Save();
            return Ok();
        }

    }
}
