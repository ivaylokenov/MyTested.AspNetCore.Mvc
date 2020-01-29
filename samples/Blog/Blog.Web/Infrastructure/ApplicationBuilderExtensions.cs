namespace Blog.Web.Infrastructure
{
    using System;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using Controllers;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
            => app.SeedDataAsync().GetAwaiter().GetResult();

        public static IApplicationBuilder UseMvcWithAreas(this IApplicationBuilder app)
            => app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRazorPages();
            });

        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var dbContext = services.GetService<BlogDbContext>();

                await dbContext.Database.MigrateAsync();

                var roleManager = services.GetService<RoleManager<IdentityRole>>();
                var existingRole = await roleManager.FindByNameAsync(ControllerConstants.AdministratorRole);
                if (existingRole != null)
                {
                    return app;
                }

                var adminRole = new IdentityRole(ControllerConstants.AdministratorRole);

                await roleManager.CreateAsync(adminRole);

                var adminUser = new User
                {
                    UserName = "admin@blog.com",
                    Email = "admin@blog.com",
                    SecurityStamp = "RandomSecurityStamp"
                };

                var userManager = services.GetService<UserManager<User>>();

                await userManager.CreateAsync(adminUser, "adminpass");

                await userManager.AddToRoleAsync(adminUser, ControllerConstants.AdministratorRole);

                var user = new User
                {
                    UserName = "normal@blog.com",
                    Email = "normal@blog.com",
                    SecurityStamp = "AnotherRandomSecurityStamp"
                };

                await userManager.CreateAsync(user, "password");

                for (int i = 0; i < 10; i++)
                {
                    var article = new Article
                    {
                        Content = $"Article content details {i}",
                        Title = $"Article {i}",
                        PublishedOn = DateTime.UtcNow,
                        IsPublic = true,
                        UserId = user.Id
                    };

                    dbContext.Articles.Add(article);
                }

                await dbContext.SaveChangesAsync();
            }

            return app;
        }
    }
}
