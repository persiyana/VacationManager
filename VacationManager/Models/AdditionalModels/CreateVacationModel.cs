using System.ComponentModel.DataAnnotations;
using VacationManager.Models.Enums;

namespace VacationManager.Models.AdditionalModels
{
    public class CreateVacationModel
    {
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestCreationDate { get; set; }
        public bool HalfDayVacation { get; set; }
        public VacationOption VacationOption { get; set; }
        public IFormFile Image { get; set; }
    }
}
