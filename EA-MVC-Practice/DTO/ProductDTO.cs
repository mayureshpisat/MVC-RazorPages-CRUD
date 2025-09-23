using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace EA_MVC_Practice.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [RegularExpression("^[a-zA-Z0-9\\\\s]+$", ErrorMessage = "Only Aplhanumeric characters allowed for Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Only Aplhanumeric characters allowed for Description")]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
