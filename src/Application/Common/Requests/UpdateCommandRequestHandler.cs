using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Requests;
public abstract class UpdateCommandRequestHandler<TRequest, TEntity, TEntityKey> : CommandRequestWithoutResponseHandler<TRequest>  where TEntityKey : struct where TEntity : BaseEntity<TEntityKey> where TRequest : IRequest
{
    protected UpdateCommandRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected abstract Func<TRequest, CancellationToken, Task<TEntity>> FindEntityToUpdateAsync { get; }
    protected abstract Action<TRequest, TEntity> MapRequestToEntity { get; }

    public async override Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await FindEntityToUpdateAsync(request, cancellationToken);
            if (entity == null) { throw new NotFoundException(typeof(TEntity).Name, entity?.Id); }

            MapRequestToEntity(request, entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in UpdateCommandRequestHandler: {ex}.", ex);
            throw;
        }
    }
}
