using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Requests.Handlers.Commands;
public abstract class UpdateCommandRequestHandler<TRequest, TEntity, TEntityKey> : CommandRequestHandler<TRequest> where TEntityKey : struct where TEntity : BaseEntity<TEntityKey> where TRequest : IRequest
{
    protected UpdateCommandRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected abstract Task<TEntity?> FindEntityAsync(TRequest request,
        CancellationToken cancellationToken = default);
    protected abstract Task MapRequestToEntityAsync(TRequest request, TEntity entity);

    public async override Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await FindEntityAsync(request, cancellationToken);
            if (entity == null) { throw new NotFoundException(typeof(TEntity).Name); }

            await MapRequestToEntityAsync(request, entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in UpdateCommandRequestHandler: {ex}.", ex);
            throw;
        }
    }
}
