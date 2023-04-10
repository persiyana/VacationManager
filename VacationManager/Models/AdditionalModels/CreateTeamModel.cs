using Microsoft.AspNetCore.Mvc.Rendering;

namespace VacationManager.Models.AdditionalModels
{
    public class CreateTeamModel
    {
        public List<SelectListItem> SelectItems { get; set; }
        public string SelectedUserId { get; set; }
    }
}
