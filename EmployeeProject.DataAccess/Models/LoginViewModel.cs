using System.ComponentModel.DataAnnotations;

namespace EmployeeProject.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        public long MobileNumber { get; set; }
    }
}
