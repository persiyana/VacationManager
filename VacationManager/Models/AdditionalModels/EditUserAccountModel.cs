using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models.AdditionalModels
{
    public class EditUserAccountModel
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Username { get; set; }

    }
}
