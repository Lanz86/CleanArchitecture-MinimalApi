﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Requests.Handlers.Queries;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.TodoLists.Queries.ExportTodos;

public record ExportTodosQuery : IRequest<ExportTodosVm>
{
    public int ListId { get; init; }
}

public class ExportTodosQueryHandler : QueryRequestHandler<ExportTodosQuery, ExportTodosVm, TodoItem>
{
    private readonly ICsvFileBuilder _fileBuilder;
    private readonly IMapper _mapper;

    public ExportTodosQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _fileBuilder = _serviceScope.ServiceProvider.GetRequiredService<ICsvFileBuilder>();
        _mapper = _serviceScope.ServiceProvider.GetRequiredService<IMapper>(); ;
    }
    
    public async override Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
    {
        var records = await DataQuery(request).ProjectTo<TodoItemRecord>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var vm = new ExportTodosVm(
            "TodoItems.csv",
            "text/csv",
            _fileBuilder.BuildTodoItemsFile(records));

        return vm;
    }
    
    public override IQueryable<TodoItem> DataQuery(ExportTodosQuery request)
    {
        return _context.TodoItems
            .Where(t => t.ListId == request.ListId);
    }
}
