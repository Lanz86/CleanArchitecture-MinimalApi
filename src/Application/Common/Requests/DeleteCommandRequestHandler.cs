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
public abstract class DeleteCommandRequestHandler<TRequest, TEntity, TEntityKey> : CommandRequestWithoutResponseHandler<TRequest>  where TEntityKey : struct where TEntity : BaseEntity<TEntityKey> where TRequest : IRequest
{
    protected DeleteCommandRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected abstract Func<TRequest, CancellationToken, Task<TEntity>> FindEntityToUpdateAsync { get; }

    public async override Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await FindEntityToUpdateAsync(request, cancellationToken);
            if (entity == null) { throw new NotFoundException(typeof(TEntity).Name, entity?.Id); }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in UpdateCommandRequestHandler: {ex}.", ex);
            throw;
        }
    }
}
