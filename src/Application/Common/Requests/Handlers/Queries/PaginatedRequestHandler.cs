using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Requests.Queries;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Common.Requests.Handlers.Queries;
public abstract class PaginatedRequestHandler<TRequest, TResponse, TEntity> : QueryRequestHandler<TRequest, PaginatedList<TResponse>, TEntity> where TResponse : class where TEntity : class where TRequest : PaginatedRequest<TResponse>
{
    protected PaginatedRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async override Task<PaginatedList<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await DataQuery(request).AsNoTracking().ProjectTo<TResponse>(_mapper.ConfigurationProvider).PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in to paginate {ex}", ex);
            throw;
        }
    }
}
