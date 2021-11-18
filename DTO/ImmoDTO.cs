using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CreateImmoDTO
    {
        [Required(ErrorMessage = "Geef straatnaam in.")]
        public string Street { get; set; }

        [MaxLength(12, ErrorMessage = "Bijv. 5")]
        [Required(ErrorMessage = "Geef Huisnummer in.")]
        public string HouseNumber { get; set; }

        [Range(1000, 9999, ErrorMessage = "Postcode moet tussen zijn 1000 en 9999 zijn.")]
        [Required(ErrorMessage = "Geef Postcode in.")]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Geef Gemeente in.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Geef Provincie in.")]
        public string Province { get; set; }

        //[Required(ErrorMessage = "Te huur/te koop.")]
        //public string Status { get; set; }

        [Required(ErrorMessage = "Geef aantal slaapkamers in.")]
        public string Rooms { get; set; }

        [Required(ErrorMessage = "Geef aantal badkamers in.")]
        public string Bathroms { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Geef uw naam in.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Geef uw emailadres in.")]
        public string Email { get; set; }

        public string Phone { get; set; }
    }

    public class ImmoDTO : CreateImmoDTO
    {
        public int ImmoId { get; set; }

        //public virtual ICollection<ImageDTO> Images { get; set; }
    }
}
