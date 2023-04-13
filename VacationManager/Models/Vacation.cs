using System.ComponentModel.DataAnnotations;
using VacationManager.Models.Enums;

namespace VacationManager.Models
{
    public class Vacation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "User is required.")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        [Display(Name ="Start date:")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }
        public DateTime RequestCreationDate { get; set; }
        public bool HalfDayVacation { get; set; }
        public bool Approved { get; set; }
        public VacationOption VacationOption { get; set; } 
        public string? FilePath { get; set; }
    }
}
