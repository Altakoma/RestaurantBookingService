using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.ServicesConfigurations;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Repositories;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace IdentityService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTSecret")!);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false,
            };

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParams;
            });

            builder.Services.AddSingleton(tokenValidationParams);
            builder.Services.AddSingleton<Seed>();

            builder.Services.AddMapper();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            builder.Services.AddTransient<ITokenGenerator, JwtGenerator>();

            var app = builder.Build();

            Seed seed = app.Services.GetRequiredService<Seed>();
            seed.SeedData();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}