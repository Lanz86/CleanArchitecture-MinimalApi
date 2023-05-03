using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Commands;
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


    protected override async Task<TodoItem?> FindEntityAsync(UpdateTodoItemCommand request, CancellationToken cancellationToken = default)
    {
        return await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);
    }

    protected override Task MapRequestToEntityAsync(UpdateTodoItemCommand request, TodoItem entity)
    {
        entity.Title = request.Title;
        entity.Done = request.Done;

        return Task.CompletedTask;
    }
}
