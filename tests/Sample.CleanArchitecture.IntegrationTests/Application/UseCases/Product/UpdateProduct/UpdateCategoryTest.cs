using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;
using Sample.CleanArchitecture.Infra.Data.EF;
using ApplicationUseCases = Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;
using Repository = Sample.CleanArchitecture.Infra.Data.EF.Repositories;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.UpdateProduct;

[Collection(nameof(UpdateProductTestFixture))]
public class UpdateProductTest
{
    private readonly UpdateProductTestFixture _fixture;

    public UpdateProductTest(UpdateProductTestFixture fixture)
        => _fixture = fixture;

    [Theory(DisplayName = nameof(UpdateProduct))]
    [Trait("Integration/Application", "UpdateProduct - Use Cases")]
    [MemberData(
    nameof(UpdateProductTestDataGenerator.GetProductsToUpdate),
    parameters: 5,
    MemberType = typeof(UpdateProductTestDataGenerator)
)]
    public async Task UpdateProduct(
    DomainEntity.Product exampleProduct,
    UpdateProductInput input
)
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        await dbContext.AddRangeAsync(_fixture.GetExampleProductList());
        var trackingInfo = await dbContext.AddAsync(exampleProduct);
        dbContext.SaveChanges();
        trackingInfo.State = EntityState.Detached;
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.UpdateProduct(
            repository, unitOfWork
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbProduct = await (_fixture.CreateDbContext(dataBase))
            .Product.FindAsync(output.ProductId);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(input.Name);
        dbProduct.Description.Should().Be(input.Description);
        dbProduct.Price.Should().Be(input.Price);
        dbProduct.IsActive.Should().Be((bool)input.IsActive!);
        dbProduct.CreatedAt.Should().Be(output.CreatedAt);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.Price.Should().Be(input.Price);
        output.IsActive.Should().Be((bool)input.IsActive!);
    }

    [Theory(DisplayName = nameof(UpdateProductWithoutIsActive))]
    [Trait("Integration/Application", "UpdateProduct - Use Cases")]
    [MemberData(
       nameof(UpdateProductTestDataGenerator.GetProductsToUpdate),
       parameters: 5,
       MemberType = typeof(UpdateProductTestDataGenerator)
   )]
    public async Task UpdateProductWithoutIsActive(
       DomainEntity.Product exampleProduct,
       UpdateProductInput exampleInput
   )
    {
        var input = new UpdateProductInput(
            exampleInput.ProductId,
            exampleInput.Name,
            exampleInput.Description,
            exampleInput.Price,
            exampleInput.IsActive
        );
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        await dbContext.AddRangeAsync(_fixture.GetExampleProductList());
        var trackingInfo = await dbContext.AddAsync(exampleProduct);
        dbContext.SaveChanges();
        trackingInfo.State = EntityState.Detached;
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.UpdateProduct(
           repository, unitOfWork
       );

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbProduct= await (_fixture.CreateDbContext(dataBase))
            .Product.FindAsync(output.ProductId);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(input.Name);
        dbProduct.Description.Should().Be(input.Description);
        dbProduct.Price.Should().Be(input.Price);
        dbProduct.CreatedAt.Should().Be(output.CreatedAt);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.Price.Should().Be(input.Price);
    }
}
