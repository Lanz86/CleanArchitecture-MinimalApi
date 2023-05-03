using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler : CreateCommandRequestHandler<CreateTodoItemCommand, int, TodoItem>
{
    public CreateTodoItemCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    
    protected override TodoItem MapRequestToEntity(CreateTodoItemCommand request)
    {
        var entity = new TodoItem { ListId = request.ListId, Title = request.Title, Done = false };
        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        return entity;
    }
}
