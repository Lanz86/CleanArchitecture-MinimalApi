﻿using TypeTest.WebApi.WebApi.Filters;

namespace TypeTest.WebApi.WebApi.Infrastructure;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithDefaults(this RouteHandlerBuilder builder, AbstractEndpoint endpoint)
    {
        return builder
            .WithName($"{endpoint.Group}_{endpoint.Name}")
            .WithGroupName(endpoint.Group)
            .WithTags(endpoint.Group)
            .RequireAuthorization()
            .WithOpenApi()
            .AddEndpointFilter<ApiExceptionFilter>();
    }
}