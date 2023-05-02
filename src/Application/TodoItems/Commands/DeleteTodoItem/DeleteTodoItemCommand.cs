using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests;
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

    protected override Func<DeleteTodoItemCommand, CancellationToken, Task<TodoItem>> FindEntityToUpdateAsync =>
        async (request, cancellationToken) =>
        {
            return await _context.TodoItems
                .FindAsync(new object[] { request.Id }, cancellationToken);
        };


}
