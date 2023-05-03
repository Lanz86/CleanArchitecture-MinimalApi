using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Requests.Handlers.Queries;
public abstract class SingleQueryRequestHandler<TRequest, TResponse, TEntity> : QueryRequestHandler<TRequest, TResponse, TEntity> where TResponse : class where TEntity : class where TRequest : IRequest<TResponse>
{
    protected SingleQueryRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await DataQuery(request).AsNoTracking().ProjectTo<TResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in to SingleQueryRequestHandler {ex}", ex);
            throw;
        }
    }
}
