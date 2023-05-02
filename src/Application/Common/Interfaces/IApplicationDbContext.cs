using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
