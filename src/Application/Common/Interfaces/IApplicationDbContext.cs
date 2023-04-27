using Microsoft.EntityFrameworkCore;

namespace TypeTest.WebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
