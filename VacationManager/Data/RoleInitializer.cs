using Microsoft.AspNetCore.Identity;

namespace VacationManager.Data
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "Unassigned", "Developer", "Team Lead", "CEO" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        
        public static async Task RemoveDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var rolesToRemove = new List<string> { "admin", "user", "ceo" };

            foreach (var role in rolesToRemove)
            {
                var identityRole = await roleManager.FindByNameAsync(role);
                if (identityRole != null)
                {
                    await roleManager.DeleteAsync(identityRole);
                }
            }
        }
    }
}
