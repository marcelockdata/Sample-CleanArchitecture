using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Sample.CleanArchitecture.Infra.Data.EF;

[ExcludeFromCodeCoverage]
public class AppDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Server=localhost;Port=25432;Database=database-dev;User Id=admin;Password=123456;");
        return new AppDbContext(optionsBuilder.Options);
    }
}
