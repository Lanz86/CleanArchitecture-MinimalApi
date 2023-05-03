using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Commands;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;

public record DeleteTodoListCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : DeleteCommandRequestHandler<DeleteTodoListCommand, TodoList, int>
{
    public DeleteTodoListCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    protected override async Task<TodoList?> FindEntityAsync(DeleteTodoListCommand request, CancellationToken cancellationToken = default)
    {
        return await _context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
