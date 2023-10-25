using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderService.Presentation.Configurations
{
    public static class JwtTokenAuthConfiguration
    {
        public static IServiceCollection AddJwtTokenAuthConfiguration(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            byte[] key;

            if (builder.Environment.IsDevelopment())
            {
                key = Encoding.UTF8.GetBytes(builder.Configuration["JWTSecret"]!);
            }
            else
            {
                key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTSecret")!);
            }

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParams;
            });

            return services;
        }
    }
}
