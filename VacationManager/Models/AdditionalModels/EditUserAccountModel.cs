using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models.AdditionalModels
{
    public class EditUserAccountModel
    {
        public string? UserId { get; set; }
        [Display(Name = "First name:")]
        public string? FirstName { get; set; }
        [Display(Name = "Last name:")]
        public string? LastName { get; set; }
        [EmailAddress]
        [Display(Name = "Email:")]
        public string? Email { get; set; }
        [Display(Name = "Username:")]
        public string? Username { get; set; }

    }
}
