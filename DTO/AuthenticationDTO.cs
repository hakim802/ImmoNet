using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AuthenticationDTO
    {
        [Required(ErrorMessage = "Emailadres is verplicht")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Ingegeven emailadres is niet geldig.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Paswoord is verplicht.")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
