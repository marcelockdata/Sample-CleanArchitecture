using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Sample.CleanArchitecture.Application.Exceptions;
using Sample.CleanArchitecture.Application.UseCases.Product.DeleteProduct;
using Sample.CleanArchitecture.Infra.Data.EF;
using ApplicationUseCases = Sample.CleanArchitecture.Application.UseCases.Product.DeleteProduct;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;
using Repository = Sample.CleanArchitecture.Infra.Data.EF.Repositories;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.DeleteProduct;

[Collection(nameof(DeleteProductTestFixture))]
public class DeleteProductTest
{
    private readonly DeleteProductTestFixture _fixture;

    public DeleteProductTest(DeleteProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteProduct))]
    [Trait("Integration/Application", "DeleteProduct - Use Cases")]
    public async Task DeleteProduct()
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        var productExample = _fixture.GetValidProduct();
        var exampleList = _fixture.GetExampleProductList(10);
        await dbContext.AddRangeAsync(exampleList);
        var tracking = await dbContext.AddAsync(productExample);
        await dbContext.SaveChangesAsync();
        tracking.State = EntityState.Detached;
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.DeleteProduct(
            repository, unitOfWork
        );
        var input = new DeleteProductInput(productExample.ProductId);

        await useCase.Handle(input, CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(dataBase);
        var dbProductDeleted = await assertDbContext.Product
            .FindAsync(productExample.ProductId);
        dbProductDeleted.Should().BeNull();
        var dbProducts = await assertDbContext
            .Product.ToListAsync();
        dbProducts.Should().HaveCount(exampleList.Count);
    }

    [Fact(DisplayName = nameof(DeleteProductThrowsWhenNotFound))]
    [Trait("Integration/Application", "DeleteProduct - Use Cases")]
    public async Task DeleteProductThrowsWhenNotFound()
    {
        var dbContext = _fixture.CreateDbContext(Guid.NewGuid());
        var exampleList = _fixture.GetExampleProductList(10);
        await dbContext.AddRangeAsync(exampleList);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.DeleteProduct(
            repository, unitOfWork
        );
        var input = new DeleteProductInput(Guid.NewGuid());

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Product '{input.Id}' not found.");

    }
}
