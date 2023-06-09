using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;

namespace CleanArchitecture.WebApi.TodoItems;

public class CreateTodoItem : AbstractEndpoint
{
    public override void Map(WebApplication app)
    {
        MapPost(app,
            async (ISender sender, CreateTodoItemCommand command) => await sender.Send(command));
    }
}
