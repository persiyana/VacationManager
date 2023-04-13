using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models.AdditionalModels
{
    public class AddUserToTeamModel
    {
        public string? UserId { get; set; }
        [Display(Name = "Username:")]
        public string? UserName { get; set; }
        [Display(Name = "Email:")]
        public string Email { get; set; }
        public int TeamId { get; set; }
        [Display(Name = "Team:")]
        public string? TeamName { get; set; }
    }
}
