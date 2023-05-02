using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Requests;

public abstract class CreateCommandRequestHandler<TRequest, TResponse, TEntity> : CommandRequestHandler<TRequest, TResponse> where TResponse : struct where TEntity : BaseEntity<TResponse> where TRequest : IRequest<TResponse>
{
    protected CreateCommandRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    { 
    }

    protected abstract Func<TRequest, TEntity> MapRequestToEntity { get; }

    public async override Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = MapRequestToEntity(request);
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in CreateCommandRequestHandler: {ex}", ex);
            throw;
        }
    }
}