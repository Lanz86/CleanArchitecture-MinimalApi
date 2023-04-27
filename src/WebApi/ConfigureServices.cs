using TypeTest.WebApi.Application.Common.Interfaces;
using TypeTest.WebApi.Infrastructure.Identity;
using TypeTest.WebApi.Infrastructure.Persistence;
using TypeTest.WebApi.WebApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using OpenIddict.Validation.AspNetCore;

namespace TypeTest.WebApi.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {

        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders().AddDefaultUI();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
        services.AddControllersWithViews();
        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "CleanArchitecture API";

            configure.DocumentProcessors.Add(new SecurityDefinitionAppender("OAuth2", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flow = OpenApiOAuth2Flow.AccessCode,
                    AuthorizationUrl = "https://localhost:5001/connect/authorize",
                    TokenUrl = "https://localhost:5001/connect/token",
                    Scopes = new Dictionary<string, string> { { "api", "api" } },
                }));
        });
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt => opt.LoginPath = "/Identity/Account/Login");
        return services;
    }
}
