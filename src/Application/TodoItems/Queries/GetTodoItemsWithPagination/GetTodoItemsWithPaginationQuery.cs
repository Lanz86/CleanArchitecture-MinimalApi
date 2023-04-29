using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Requests;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : PaginatedRequest<TodoItemBriefDto>
{
    public int ListId { get; init; }
}

public class GetTodoItemsWithPaginationQueryHandler : PaginatedRequestHandler<GetTodoItemsWithPaginationQuery, TodoItemBriefDto, TodoItem>
{

    public GetTodoItemsWithPaginationQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {

    }


    public override Func<GetTodoItemsWithPaginationQuery, IQueryable<TodoItem>> Query =>
        request =>
        {
            return _context.TodoItems
                .Where(x => x.ListId == request.ListId)
                .OrderBy(x => x.Title);
        };
}
