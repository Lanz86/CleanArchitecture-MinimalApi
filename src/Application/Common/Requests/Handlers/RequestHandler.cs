using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Commands;
using MediatR;

namespace CleanArchitecture.Application.Common.Requests.Handlers;

public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>, IDisposable
    where TRequest : IRequest
{
    protected readonly IApplicationDbContext _context;
    protected readonly ILogger<RequestHandler<TRequest>> _logger;
    private readonly IServiceScope _serviceScope;

    protected RequestHandler(IServiceProvider serviceProvider)
    {
        _serviceScope = serviceProvider.CreateScope();
        _context = _serviceScope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _logger = _serviceScope.ServiceProvider.GetRequiredService<ILogger<RequestHandler<TRequest>>>();
    }

    public abstract Task Handle(TRequest request, CancellationToken cancellationToken);

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}

public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>, IDisposable
    where TRequest : IRequest<TResponse>
{
    protected readonly IApplicationDbContext _context;
    protected readonly ILogger<CommandRequestHandler<TRequest, TResponse>> _logger;
    protected readonly IServiceScope _serviceScope;

    protected RequestHandler(IServiceProvider serviceProvider)
    {
        _serviceScope = serviceProvider.CreateScope();
        _context = _serviceScope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _logger = _serviceScope.ServiceProvider.GetRequiredService<ILogger<CommandRequestHandler<TRequest, TResponse>>>();
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}
