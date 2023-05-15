using Microsoft.AspNetCore.Identity;

namespace LectureManager.Models
{
    public static class IdentityHelper
    {
        public const string Instructor = "Instrctor";
        public const string Student = "Student";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            // Setup role manager
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            // Run through all roles
            foreach(string currRole in roles)
            {
                // Check if the current role exists
                bool doesRoleExist = await roleManager.RoleExistsAsync(currRole);

                // If the roles does not exist
                if (!doesRoleExist)
                {
                    // Create it
                    await roleManager.CreateAsync(new IdentityRole(currRole));
                }
            }
        }
    }
}
