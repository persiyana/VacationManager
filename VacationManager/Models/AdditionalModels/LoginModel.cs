using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models.AdditionalModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
