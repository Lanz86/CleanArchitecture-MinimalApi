using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Commands;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : UpdateCommandRequestHandler<UpdateTodoListCommand, TodoList, int>
{
    public UpdateTodoListCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }


    protected override async Task<TodoList?> FindEntityAsync(UpdateTodoListCommand request, CancellationToken cancellationToken = default)
    {
        return await _context.TodoLists
            .FindAsync(new object[] { request.Id }, cancellationToken);
    }

    protected override Task MapRequestToEntityAsync(UpdateTodoListCommand request, TodoList entity)
    {
        entity.Title = request.Title;

        return Task.CompletedTask;
    }
}
