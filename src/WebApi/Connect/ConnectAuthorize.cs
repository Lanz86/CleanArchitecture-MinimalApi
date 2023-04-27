using System.Security.Claims;
using Azure.Core;
using CleanArchitecture.WebApi.Authorization;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;

namespace CleanArchitecture.WebApi.Connect;

public class ConnectAuthorize : AbstractEndpoint
{
    public override void Map(WebApplication app)
    {
        app.MapMethods("/connect/authorize", new[] { "GET", "POST" }, connect_Authorize);
        //MapGet(app, "~/connect/authorize", connect_Authorize);
        //MapPost(app, "~/connect/authorize", connect_Authorize);
    }

    private async Task<IResult> connect_Authorize(HttpContext context, HttpRequest httpRequest,
        IOpenIddictScopeManager manager)
    {
        var request = context.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var result = await context.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        if (!result.Succeeded)
        {
            return Results.Challenge(authenticationSchemes: new List<string> { CookieAuthenticationDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties()
                {
                    RedirectUri = httpRequest.PathBase + httpRequest.Path + QueryString.Create(
                        httpRequest.HasFormContentType ? httpRequest.Form.ToList() : httpRequest.Query.ToList()
                    )
                });

        }

        // Create a new claims principal
        var claims = new List<Claim>
        {
            // 'subject' claim which is required
            new Claim(OpenIddictConstants.Claims.Subject, result.Principal.Identity.Name),
            new Claim("some claim", "some value").SetDestinations(OpenIddictConstants.Destinations.AccessToken),
            //new Claim(OpenIddictConstants.Claims.Email, result.Principal.Identity.Name).SetDestinations(OpenIddictConstants.Destinations.IdentityToken)

        };

        var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Set requested scopes (this is not done automatically)
        claimsPrincipal.SetScopes(request.GetScopes());

        return Results.SignIn(claimsPrincipal,
              authenticationScheme:  OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
    );
}
}
