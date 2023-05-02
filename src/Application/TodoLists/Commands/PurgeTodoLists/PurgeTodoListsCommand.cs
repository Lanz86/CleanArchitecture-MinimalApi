using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : CommandRequestWithoutResponseHandler<PurgeTodoListsCommand>
{
    public PurgeTodoListsCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.TodoLists.RemoveRange(_context.TodoLists);

        await _context.SaveChangesAsync(cancellationToken);
    }


}
