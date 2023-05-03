using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : DeleteCommandRequestHandler<DeleteTodoItemCommand, TodoItem, int>
{
    public DeleteTodoItemCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override async Task<TodoItem?> FindEntityAsync(DeleteTodoItemCommand request, CancellationToken cancellationToken = default)
    {
        return await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);
    }
}
