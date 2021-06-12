namespace OnLineBookStore.Server.Infrastructure
{
    using Data;
    using Data.Models;
    using System.Text;
    using Features.Identity.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Features.Cart.Services;
    using Features.Management.Services;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ICartService, CartService>()
                .AddTransient<IManagementService, ManagementService>();


        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<OnLineBookStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequiredUniqueChars = 0;
                    options.User.RequireUniqueEmail = true;
                    
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<OnLineBookStoreDbContext>();
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        { 
            
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }

        public static AppSettings GetAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsConfiguration = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfiguration);
            var appSettings = appSettingsConfiguration.Get<AppSettings>();
            return appSettings;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My OnLineBookStores API", Version = "v1" });
            });
        
    }
}
