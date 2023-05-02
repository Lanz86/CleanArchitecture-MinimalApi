using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Common.Requests;
public abstract class CommandRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>, IDisposable where TRequest : IRequest<TResponse>
{
    protected readonly IApplicationDbContext _context;
    protected readonly ILogger<CommandRequestHandler<TRequest, TResponse>> _logger;
    private readonly IServiceScope _serviceScope;
    protected CommandRequestHandler(IServiceProvider serviceProvider)
    {
        _serviceScope = serviceProvider .CreateScope();
        _context = _serviceScope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _logger = _serviceScope.ServiceProvider.GetRequiredService<ILogger<CommandRequestHandler<TRequest, TResponse>>>();
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}
