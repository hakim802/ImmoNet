using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContactDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Naam is een verplicht veld.")]
        [MaxLength(100, ErrorMessage = "Maximum toegelaten tekens is 100.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Email is een verplicht veld.")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
