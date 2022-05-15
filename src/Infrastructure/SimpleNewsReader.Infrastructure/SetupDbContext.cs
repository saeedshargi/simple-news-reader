using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleNewsReader.Infrastructure.Data;

namespace SimpleNewsReader.Infrastructure;

public static class SetupDbContext
{
    public static void AddDbContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<SimpleNewsReaderContext>(options => options.UseSqlServer(connectionString));
    }
}