using Microsoft.AspNetCore.Identity;

namespace LectureManager.Models
{
    public static class IdentityHelper
    {
        /// <summary>
        /// Represents the Instructor role
        /// </summary>
        public const string Instructor = "Instrctor";

        /// <summary>
        /// Represents the Student role
        /// </summary>
        public const string Student = "Student";

        /// <summary>
        /// When called: Sets up all the site roles
        /// </summary>
        /// <param name="provider">A Service Provider</param>
        /// <param name="roles">An array containing all site roles</param>
        public static async Task SetupRoles(IServiceProvider provider, params string[] roles)
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

        /// <summary>
        /// Creates a default user for the given role as long as one does not already exist
        /// </summary>
        /// <param name="provider">A Service Provider</param>
        /// <param name="role">The role the default user would belong to</param>
        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            // Setup user manager
            UserManager<IdentityUser> userManager = provider.GetService<UserManager<IdentityUser>>();

            // Get the number of users belonging to the current role
            int numUsersInRole = (await userManager.GetUsersInRoleAsync(role)).Count;

            // If no users exist in that role
            if(numUsersInRole == 0)
            {
                // Setup the default user for that role
                var defaultUser = new IdentityUser()
                {
                    Email = "instructor@lecturemanager.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "Pas.sWork5ngHr$der");

                // Make them the given role
                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
