using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models.AdditionalModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string? Id { get; set; }

        public string? RoleName { get; set; }
        public SelectList? Roles { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public List<string>? Users { get; set; }

    }
}
