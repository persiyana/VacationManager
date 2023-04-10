using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;

namespace VacationManager.Models.AdditionalModels
{
    public class CeoIndexModel
    {
        public List<ApplicationUser> Users { get; set; }

        public Dictionary<ApplicationUser, string> UserRoles { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public CeoIndexModel(UserManager<ApplicationUser> usermanager) {
            _userManager = usermanager;
            
        }
        public async Task AddToList()
        {
            foreach (var u in Users)
            {
                string s = _userManager.GetRolesAsync(u).Result[0];
                UserRoles.Add(u, s);
            }

        }
        public List<Team> Teams { get; set; }
    }
}
