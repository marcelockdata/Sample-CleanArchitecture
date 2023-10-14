using Bogus;
using Microsoft.EntityFrameworkCore;
using Sample.CleanArchitecture.Infra.Data.EF;

namespace Sample.CleanArchitecture.IntegrationTests.Base;

public abstract class BaseFixture
{
    public Faker Faker { get; set; }

    protected BaseFixture()
        => Faker = new Faker("pt_BR");

    public bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public AppDbContext CreateDbContext(Guid database)
    {
        var context = new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: database.ToString())
            .Options
        );
        return context;
    }
}
