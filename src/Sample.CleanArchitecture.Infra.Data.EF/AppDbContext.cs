using Microsoft.EntityFrameworkCore;
using Sample.CleanArchitecture.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Sample.CleanArchitecture.Infra.Data.EF;

[ExcludeFromCodeCoverage]
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    public DbSet<Product> Product { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
     => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
          => configurationBuilder
              .Properties<string>()
              .AreUnicode(false)
              .HaveMaxLength(255);
}

