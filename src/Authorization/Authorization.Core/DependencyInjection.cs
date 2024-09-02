using Authorization.Core.Interfaces;
using Authorization.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Refit;

namespace Authorization.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJWTLayer(this IServiceCollection services)
        {
            services.AddRefitClient<IUserRequests>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:4088/api"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTService.JWT_SECURITY_KEY)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}