using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models
{
    public class Team
    {
        public Team(){
            ApplicationUsers = new List<ApplicationUser>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name:")]
        public string Name { get; set; }
        [Display(Name = "Project:")]
        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        [Required(ErrorMessage = "Leader is required.")]
        [Display(Name = "Leader:")]
        public string LeaderId { get; set; }
        public virtual ApplicationUser? Leader { get; set; }
        public virtual ICollection<ApplicationUser>? ApplicationUsers { get; set; }
    }
}
