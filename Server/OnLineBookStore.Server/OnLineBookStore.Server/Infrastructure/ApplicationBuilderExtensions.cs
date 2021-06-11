using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnLineBookStore.Server.Data;

namespace OnLineBookStore.Server.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddSwaggerUI(this IApplicationBuilder app) =>
            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("swagger/v1/swagger.json", "My OnLineBookStore API");
                    c.RoutePrefix = string.Empty;
                });
        

        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dBContext =  services.ServiceProvider.GetService<OnLineBookStoreDbContext>();
            
            dBContext.Database.Migrate();

            return app;
        }

        public static void ApplyRoles(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<OnLineBookStoreDbContext>();

            if (context.Roles.Count() <= 1)
            {
                context.Roles.Add(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
                context.Roles.Add(new IdentityRole() { Name = "User", NormalizedName = "USER" });
                context.SaveChanges();
            }
        }
    }
}
