using TypeTest.WebApi.Application.Common.Interfaces;

namespace TypeTest.WebApi.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
