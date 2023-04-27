namespace CleanArchitecture.WebApi.Infrastructure;

public abstract class AbstractEndpoint : IEndpoint
{
    public abstract void Map(WebApplication app);

    public string Name => GetType().Name;

    public string Group => GetType().Namespace!.Split(".")[^1];

    public virtual string BaseRoute => $"/api/{Group}/";

    public RouteHandlerBuilder MapGet(WebApplication app, Delegate handler) => MapGet(app, "", handler);

    public RouteHandlerBuilder MapGet(WebApplication app, string pattern, Delegate handler, bool withDefaults = true)
    {
        var builder = app.MapGet(BuildRoutePattern(pattern), handler);

        if (withDefaults)
        {
            builder.WithDefaults(this);
        }

        return builder;
    }

    public RouteHandlerBuilder MapPost(WebApplication app, Delegate handler) => MapPost(app, "", handler);

    public RouteHandlerBuilder MapPost(WebApplication app, string pattern, Delegate handler)
    {
        return app.MapPost(BuildRoutePattern(pattern), handler)
            .WithDefaults(this);
    }

    public RouteHandlerBuilder MapPut(WebApplication app, Delegate handler) => MapPut(app, "", handler);

    public RouteHandlerBuilder MapPut(WebApplication app, string pattern, Delegate handler)
    {
        return app.MapPut(BuildRoutePattern(pattern), handler)
            .WithDefaults(this);
    }

    public RouteHandlerBuilder MapDelete(WebApplication app, Delegate handler) => MapDelete(app, "", handler);

    public RouteHandlerBuilder MapDelete(WebApplication app, string pattern, Delegate handler)
    {
        return app.MapDelete(BuildRoutePattern(pattern), handler)
            .WithDefaults(this);
    }

    public RouteHandlerBuilder MapMethods(WebApplication app, IEnumerable<string> httpMethods, Delegate handler) =>
        MapMethods(app, string.Empty, httpMethods, handler);

    public RouteHandlerBuilder MapMethods(WebApplication app, string pattern, IEnumerable<string> httpMethods, Delegate handler)
    {
        return app.MapMethods(BuildRoutePattern(pattern), httpMethods, handler);
    }

    private string BuildRoutePattern(string pattern)
    {
        if (!pattern.StartsWith("/"))
        {
            pattern = $"{BaseRoute}{pattern}".ToLower();
        }

        return pattern;
    }
}
