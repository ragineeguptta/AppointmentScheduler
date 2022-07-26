using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember me?")]
        public string Remember { get; set; }
    }
}
