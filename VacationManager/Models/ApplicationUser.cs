using Microsoft.AspNetCore.Identity;
using System.Data;

namespace VacationManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
