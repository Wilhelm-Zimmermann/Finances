using FinanceController.Domain.Api.Authentication;
using FinanceController.Domain.Api.Settings;
using FinanceController.Domain.Infra.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinanceController.Domain.Api.Extensions
{
    public static class Authorization
    {
        public static void ConfigAuthorizationSettings(this WebApplicationBuilder builder)
        {
            var key = Encoding.ASCII.GetBytes(TokenSettings.SecretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.Authority = builder.Configuration["IdentityServerUrl"];
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters.ValidateAudience = false;
                    x.TokenValidationParameters.NameClaimType = "username";
                });

            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddSingleton<IPermissionService, PermissionService>();
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        }
    }
}
