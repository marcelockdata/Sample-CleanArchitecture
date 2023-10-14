using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;
using Sample.CleanArchitecture.Domain.Exceptions;
using Sample.CleanArchitecture.Infra.Data.EF;
using ApplicationUseCases = Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;
using Repository = Sample.CleanArchitecture.Infra.Data.EF.Repositories;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.CreateProduct;

[Collection(nameof(CreateProductTestFixture))]
public class CreateProductTest
{
    private readonly CreateProductTestFixture _fixture;

    public CreateProductTest(CreateProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateProduct))]
    [Trait("Integration/Application", "CreateProduct - Use Cases")]
    public async void CreateProduct()
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var unitOfWor = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.CreateProduct(
            repository, unitOfWor
        );
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbProduct = await (_fixture.CreateDbContext(dataBase))
            .Product.FindAsync(output.ProductId);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(input.Name);
        dbProduct.Description.Should().Be(input.Description);
        dbProduct.Price.Should().Be(input.Price);
        dbProduct.IsActive.Should().Be(input.IsActive);
        dbProduct.CreatedAt.Should().Be(output.CreatedAt);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.Price.Should().Be(input.Price);
        output.IsActive.Should().Be(input.IsActive);
        output.ProductId.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateProduct))]
    [Trait("Integration/Application", "CreateProduct - Use Cases")]
    [MemberData(
    nameof(CreateProductTestDataGenerator.GetInvalidInputs),
    parameters: 4,
    MemberType = typeof(CreateProductTestDataGenerator)
)]
    public async void ThrowWhenCantInstantiateProduct(
    CreateProductInput input,
    string expectedExceptionMessage
)
    {
        var dataBase = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext(dataBase);
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var unitOfWor = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.CreateProduct(
            repository, unitOfWor
        );

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(expectedExceptionMessage);
        var dbProductList = _fixture.CreateDbContext(dataBase)
            .Product.AsNoTracking()
            .ToList();
        dbProductList.Should().HaveCount(0);
    }
}
