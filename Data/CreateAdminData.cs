using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProgrammasy.Constants;
using TestProgrammasy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProgrammasy.Data
{
    public static class CreateAdminData
    {
        public async static Task CreateDataTask(IHost host)
        {
            IWebHostEnvironment _env = host.Services.GetService<IWebHostEnvironment>();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await _dbContext.Database.MigrateAsync();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Admin.ToString()));
                    await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Teacher.ToString()));
                    await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Student.ToString()));
                }

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var admin = await userManager.FindByNameAsync(Authorization.default_username.ToString());
                if (admin == null)
                {
                    ApplicationUser adminUser = new ApplicationUser
                    {
                        UserName = Authorization.default_username.ToString(),
                        Email = "admin@gmail.com",
                        FirstName = "Gulshat",
                        LastName = "Mergenowa",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    await userManager.CreateAsync(adminUser, Authorization.default_password);
                    await userManager.AddToRoleAsync(adminUser, Authorization.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(adminUser, Authorization.Roles.Teacher.ToString());
                    await userManager.AddToRoleAsync(adminUser, Authorization.Roles.Student.ToString());

                }                
            }
        }
    }
}
