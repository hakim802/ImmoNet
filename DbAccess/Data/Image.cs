using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.Data
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImageSource { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Immo))]
        public int ImmoId { get; set; }

        public virtual Immo Immo { get; set; }
    }
}
