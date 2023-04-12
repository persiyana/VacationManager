using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models.AdditionalModels
{
    public class HomePageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Team { get; set; }
        public string Project { get; set; }
    }
}
