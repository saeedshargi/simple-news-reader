using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SimpleNewsReader.Infrastructure.Data;

public class SimpleNewsReaderContext : DbContext
{
    public SimpleNewsReaderContext(DbContextOptions<SimpleNewsReaderContext> options): base(options)
    {
        
    }

    public DbSet<Domain.Entities.News>? Newses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}