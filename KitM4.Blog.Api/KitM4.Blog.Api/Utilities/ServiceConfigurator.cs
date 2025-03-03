using KitM4.Blog.Core.Services;
using KitM4.Blog.Core.Validators;
using KitM4.Blog.Core.Services.Interfaces;
using KitM4.Blog.Data;
using KitM4.Blog.Data.Repositories;
using KitM4.Blog.Data.Repositories.Interfaces;
using KitM4.Blog.Domain.Configurations;
using KitM4.Blog.Domain.Communication.Requests;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using FluentValidation;

namespace KitM4.Blog.Api.Utilities;

public static class ServiceConfigurator
{
    public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<DefaultAdmin>(configuration.GetSection(nameof(DefaultAdmin)));

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IValidator<AuthRequests.Login>, LoginRequestValidator>();
        services.AddScoped<IValidator<AuthRequests.Register>, RegisterRequestValidator>();
        services.AddScoped<IValidator<AuthRequests.ChangeRole>, ChangeRoleRequestValidator>();

        return services;
    }

    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthPolicy.All, builder => builder.RequireRole(AuthPolicy.Roles[AuthPolicy.All]))
            .AddPolicy(AuthPolicy.Moderation, builder => builder.RequireRole(AuthPolicy.Roles[AuthPolicy.Moderation]))
            .AddPolicy(AuthPolicy.Admin, builder => builder.RequireRole(AuthPolicy.Roles[AuthPolicy.Admin]));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                JwtSettings jwt = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt.Key)),
                };
            });

        return services;
    }
}