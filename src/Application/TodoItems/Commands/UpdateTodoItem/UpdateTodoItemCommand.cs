using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler : UpdateCommandRequestHandler<UpdateTodoItemCommand, TodoItem, int>
{
    public UpdateTodoItemCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override Func<UpdateTodoItemCommand, CancellationToken, Task<TodoItem>> FindEntityToUpdateAsync =>
        async (request, cancellationToken) =>
        {
            return await _context.TodoItems
                .FindAsync(new object[] { request.Id }, cancellationToken);
        };

    protected override Action<UpdateTodoItemCommand, TodoItem> MapRequestToEntity => (request, entity) =>
    {
        entity.Title = request.Title;
        entity.Done = request.Done;
    };
}
