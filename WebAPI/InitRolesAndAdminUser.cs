using Microsoft.AspNetCore.Identity;

namespace WebAPI ;

public static class InitRolesAndAdminUser
{
    public static async Task InitializeAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        const string adminEmail = "stepan@mail.ru";
        const string password = "Stepan*10";

        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        var adminUser = await userManager.FindByNameAsync(adminEmail);
        if (adminUser == null)
        {
            var admin = new IdentityUser(adminEmail)
            {
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Administrator");
            }
        }
    }
}