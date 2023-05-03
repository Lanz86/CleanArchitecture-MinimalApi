using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Requests.Handlers.Queries;
public abstract class EnumerableQueryRequestHandler<TRequest, TResponse, TEntity> : QueryRequestHandler<TRequest, IEnumerable<TResponse>, TEntity> where TResponse : class where TEntity : class where TRequest : IRequest<IEnumerable<TResponse>>
{
    protected EnumerableQueryRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<IEnumerable<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await DataQuery(request).AsNoTracking().ProjectToListAsync<TResponse>(_mapper.ConfigurationProvider);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EnumerableRequestHandler {ex}", ex);
            throw;
        }
    }
}
