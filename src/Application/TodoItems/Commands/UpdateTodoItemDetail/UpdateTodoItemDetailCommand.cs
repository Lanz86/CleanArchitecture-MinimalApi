using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler : UpdateCommandRequestHandler<UpdateTodoItemDetailCommand, TodoItem, int>
{
    public UpdateTodoItemDetailCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override Func<UpdateTodoItemDetailCommand, CancellationToken, Task<TodoItem>> FindEntityToUpdateAsync =>
        async (request, cancellationToken) =>
        {
            return await _context.TodoItems
                .FindAsync(new object[] { request.Id }, cancellationToken);
        };

    protected override Action<UpdateTodoItemDetailCommand, TodoItem> MapRequestToEntity => (request, entity) =>
    {
        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;
    };
}
