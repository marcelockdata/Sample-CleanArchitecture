using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkInfra = Sample.CleanArchitecture.Infra.Data.EF;

namespace Sample.CleanArchitecture.IntegrationTests.Infra.Data.EF.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;

    public UnitOfWorkTest(UnitOfWorkTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Commit))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Commit()
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        var exampleProductList = _fixture.GetExampleProductList();
        await dbContext.AddRangeAsync(exampleProductList);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(dataBase);
        var savedProducts = assertDbContext.Product
            .AsNoTracking().ToList();
        savedProducts.Should()
            .HaveCount(exampleProductList.Count);
    }


    [Fact(DisplayName = nameof(Rollback))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Rollback()
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        var task = async ()
            => await unitOfWork.Rollback(CancellationToken.None);

        await task.Should().NotThrowAsync();
    }
}
