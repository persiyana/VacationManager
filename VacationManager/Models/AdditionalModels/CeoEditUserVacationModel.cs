using VacationManager.Models.Enums;

namespace VacationManager.Models.AdditionalModels
{
    public class CeoEditUserVacationModel
    {
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? ApplicationUserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestCreationDate { get; set; }
        public bool HalfDayVacation { get; set; }
        public bool Approved { get; set; }
        public VacationOption VacationOption { get; set; }
        public string? FilePath { get; set; }
    }
}
