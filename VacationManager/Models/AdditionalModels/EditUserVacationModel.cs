using VacationManager.Models.Enums;

namespace VacationManager.Models.AdditionalModels
{
    public class EditUserVacationModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestCreationDate { get; set; }
        public bool HalfDayVacation { get; set; }
        public bool Approved { get; set; }
        public VacationOption VacationOption { get; set; }
        public string? FilePath { get; set; }
    }
}
