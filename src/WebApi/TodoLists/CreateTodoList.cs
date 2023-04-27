using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

namespace CleanArchitecture.WebApi.TodoLists;

public class CreateTodoList : AbstractEndpoint
{
    public override void Map(WebApplication app)
    {
        MapPost(app,
            async (ISender sender, CreateTodoListCommand command) => await sender.Send(command));
    }
}
