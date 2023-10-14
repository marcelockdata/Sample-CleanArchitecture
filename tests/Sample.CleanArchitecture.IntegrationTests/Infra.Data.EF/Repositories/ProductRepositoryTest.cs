using FluentAssertions;
using Sample.CleanArchitecture.Infra.Data.EF;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;
using Repository = Sample.CleanArchitecture.Infra.Data.EF.Repositories;

namespace Sample.CleanArchitecture.IntegrationTests.Infra.Data.EF.Repositories;

[Collection(nameof(ProductRepositoryTestFixture))]
public class ProductRepositoryTest
{
    private readonly ProductRepositoryTestFixture _fixture;

    public ProductRepositoryTest(ProductRepositoryTestFixture fixture)
        => _fixture = fixture;


    [Fact(DisplayName = nameof(Insert))]
    [Trait("Integration/Infra.Data", "ProductRepository - Repositories")]
    public async Task Insert()
    {
        var dataBase = Guid.NewGuid();
        AppDbContext dbContext = _fixture.CreateDbContext(dataBase);
        var exampleProduct = _fixture.GetExampleProduct();
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);

        await repository.Add(exampleProduct);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbProduct = await (_fixture.CreateDbContext(dataBase))
            .Product.FindAsync(exampleProduct.ProductId);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(exampleProduct.Name);
        dbProduct.Description.Should().Be(exampleProduct.Description);
        dbProduct.IsActive.Should().Be(exampleProduct.IsActive);
        dbProduct.Price.Should().Be(exampleProduct.Price);
        dbProduct.CreatedAt.Should().Be(exampleProduct.CreatedAt);
    }

    [Fact(DisplayName = nameof(Get))]
    [Trait("Integration/Infra.Data", "ProductRepository - Repositories")]
    public async Task Get()
    {
        var dataBase = Guid.NewGuid();
        AppDbContext dbContext = _fixture.CreateDbContext(dataBase);
        var exampleProduct = _fixture.GetExampleProduct();
        var exampleProductList = _fixture.GetExampleProductList(15);
        exampleProductList.Add(exampleProduct);
        await dbContext.AddRangeAsync(exampleProductList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var repository = new Repository.Repository<DomainEntity.Product>(
            _fixture.CreateDbContext(dataBase)
        );

        var dbProduct = await repository.GetById(
            exampleProduct.ProductId,
            CancellationToken.None);

        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(exampleProduct.Name);
        dbProduct.Description.Should().Be(exampleProduct.Description);
        dbProduct.IsActive.Should().Be(exampleProduct.IsActive);
        dbProduct.Price.Should().Be(exampleProduct.Price);
        dbProduct.CreatedAt.Should().Be(exampleProduct.CreatedAt);
    }

    ////[Fact(DisplayName = nameof(GetThrowIfNotFound))]
    ////[Trait("Integration/Infra.Data", "ProductRepository - Repositories")]
    ////public async Task GetThrowIfNotFound()
    ////{
    ////    AppDbContext dbContext = _fixture.CreateDbContext();
    ////    var productId = Guid.NewGuid();
    ////    await dbContext.AddRangeAsync(_fixture.GetExampleProductList(15));
    ////    await dbContext.SaveChangesAsync(CancellationToken.None);
    ////    var repository = new Repository.Repository<DomainEntity.Product>(dbContext);

    ////    var task = async () => await repository.GetById(
    ////        productId,
    ////        CancellationToken.None);

    ////    await task.Should().ThrowAsync<NotFoundException>()
    ////        .WithMessage($"Product '{productId}' not found.");
    ////}
    

    [Fact(DisplayName = nameof(Update))]
    [Trait("Integration/Infra.Data", "ProductRepository - Repositories")]
    public async Task Update()
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        var exampleProduct = _fixture.GetExampleProduct();
        var newExampleProduct = _fixture.GetExampleProduct();
        var exampleProductList = _fixture.GetExampleProductList(15);
        exampleProductList.Add(exampleProduct);
        await dbContext.AddRangeAsync(exampleProductList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);

        exampleProduct.Update(newExampleProduct.Name, newExampleProduct.Description, newExampleProduct.Price);
        await repository.Update(exampleProduct);
        await dbContext.SaveChangesAsync();

        var dbProduct = await (_fixture.CreateDbContext(dataBase))
            .Product.FindAsync(exampleProduct.ProductId);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(exampleProduct.Name);
        dbProduct.Description.Should().Be(exampleProduct.Description);
        dbProduct.IsActive.Should().Be(exampleProduct.IsActive);
        dbProduct.Price.Should().Be(exampleProduct.Price);
        dbProduct.CreatedAt.Should().Be(exampleProduct.CreatedAt);
    }
}
