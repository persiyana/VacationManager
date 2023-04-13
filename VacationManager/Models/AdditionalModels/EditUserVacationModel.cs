using System.ComponentModel.DataAnnotations;
using VacationManager.Models.Enums;

namespace VacationManager.Models.AdditionalModels
{
    public class EditUserVacationModel
    {
        public int Id { get; set; }
        [Display(Name = "Start date:")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End date:")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Current date:")]
        public DateTime RequestCreationDate { get; set; }
        [Display(Name = "Half day vacation:")]
        public bool HalfDayVacation { get; set; }
        [Display(Name = "Approved:")]
        public bool Approved { get; set; }
        [Display(Name = "Vacation option:")]
        public VacationOption VacationOption { get; set; }
        [Display(Name = "File:")]
        public string? FilePath { get; set; }
    }
}
