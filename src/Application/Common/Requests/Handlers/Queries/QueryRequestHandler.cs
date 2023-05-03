using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Requests.Handlers.Queries;
public abstract class QueryRequestHandler<TRequest, TResponse, TEntity> : RequestHandler<TRequest, TResponse> where TResponse : class where TEntity : class where TRequest : IRequest<TResponse>
{
    protected readonly IMapper _mapper;

    protected QueryRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _mapper = _serviceScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    public abstract IQueryable<TEntity> DataQuery(TRequest request);
}
