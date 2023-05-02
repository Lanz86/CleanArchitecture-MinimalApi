using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : CreateCommandRequestHandler<CreateTodoListCommand, int, TodoList>
{

    public CreateTodoListCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override Func<CreateTodoListCommand, TodoList> MapRequestToEntity => (request) =>
    {
        var entity = new TodoList { Title = request.Title };

        return entity;
    };
}
