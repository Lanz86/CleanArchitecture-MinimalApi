using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Common.Requests;
public abstract class PaginatedRequestHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, PaginatedList<TResponse>> where TResponse : class where TEntity : class where TRequest : PaginatedRequest<TResponse>
{
    protected readonly IMapper _mapper;
    protected readonly ILogger<PaginatedRequestHandler<TRequest, TResponse, TEntity>> _logger;
    protected readonly IApplicationDbContext _context;

    protected PaginatedRequestHandler(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateAsyncScope();
        _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<PaginatedRequestHandler<TRequest, TResponse, TEntity>>>();
        _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    }

    public abstract Func<TRequest, IQueryable<TEntity>> Query { get; }

    public async Task<PaginatedList<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await Query(request).ProjectTo<TResponse>(_mapper.ConfigurationProvider).PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in to paginate {ex}", ex);
            throw;
        }
    }

    
}
