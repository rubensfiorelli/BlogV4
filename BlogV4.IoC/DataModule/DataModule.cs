using BlogV4.Application.Repositories;
using BlogV4.Application.Services;
using BlogV4.Data.DataContext;
using BlogV4.Data.Repositories;
using BlogV4.Domain.Repositories;
using BlogV4.IoC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogV4.IoC.DataModule
{
    public static class DataModule
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                 .AddRepositories()
                 .AddJwtServices()
                 .AddServices();


            services
                .AddDbContextPool<ApplicationDbContext>(opts => opts
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .UseSqlServer(configuration
                .GetConnectionString("SQLConnection"), b => b
                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //static IDbConnection SqlConnection()
            //    => new SqlConnection("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True");

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));


            return services;

        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserService), typeof(UserService));

            return services;

        }

        private static IServiceCollection AddJwtServices(this IServiceCollection services)
        {
            services.AddTransient<TokenService>();

            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(decrip =>
            {
                decrip.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            return services;

        }

    }
}
