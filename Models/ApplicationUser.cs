using Microsoft.AspNetCore.Identity;

namespace AppointmentScheduler.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
