using System.ComponentModel.DataAnnotations;

namespace EA_MVC_Practice.DTO
{
    public class RegisterUserDTO
    {
        [EmailAddress(ErrorMessage ="Invalid Email address")]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [StringLength(10, ErrorMessage ="Username can only be upto 10 characters")]
        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
