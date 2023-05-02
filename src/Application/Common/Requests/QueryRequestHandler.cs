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

namespace CleanArchitecture.Application.Common.Requests;
public abstract class QueryRequestHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>, IDisposable where TResponse : class where TEntity : class where TRequest : IRequest<TResponse>
{
    protected readonly IMapper _mapper;
    protected readonly ILogger<QueryRequestHandler<TRequest, TResponse, TEntity>> _logger;
    protected readonly IApplicationDbContext _context;
    protected readonly IServiceScope _serviceScope;

    protected QueryRequestHandler(IServiceProvider serviceProvider)
    {
        _serviceScope = serviceProvider.CreateScope();
        _mapper = _serviceScope.ServiceProvider.GetRequiredService<IMapper>();
        _logger = _serviceScope.ServiceProvider.GetRequiredService<ILogger<QueryRequestHandler<TRequest, TResponse, TEntity>>>();
        _context = _serviceScope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    }

    public abstract Func<TRequest, IQueryable<TEntity>> Query { get; }
    
    public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}
