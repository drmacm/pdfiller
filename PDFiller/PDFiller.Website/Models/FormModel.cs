using System.ComponentModel.DataAnnotations;

namespace PDFiller.Website.Models
{
    public class FormModel
    {
        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Name is too short or too long.")]
        public string Name { get; set; }
       
        public bool Bar { get; set; }
    }
}
