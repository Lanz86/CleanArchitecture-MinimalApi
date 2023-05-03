using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Common.Requests.Handlers.Commands;

public abstract class CommandRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected CommandRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

public abstract class CommandRequestHandler<TRequest> : RequestHandler<TRequest> where TRequest : IRequest
{
    protected CommandRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
