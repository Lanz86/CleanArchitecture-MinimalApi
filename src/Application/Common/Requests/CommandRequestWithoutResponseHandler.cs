using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Requests;
public abstract class CommandRequestWithoutResponseHandler<TRequest> : IRequestHandler<TRequest>  where TRequest : IRequest
{
    protected readonly IApplicationDbContext _context;
    protected ILogger<CommandRequestWithoutResponseHandler<TRequest>> _logger;
    
    protected CommandRequestWithoutResponseHandler(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateAsyncScope();
        _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<CommandRequestWithoutResponseHandler<TRequest>>>();
    }

    public virtual async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}