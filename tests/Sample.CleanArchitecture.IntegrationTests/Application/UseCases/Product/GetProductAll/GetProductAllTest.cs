using FluentAssertions;
using Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;
using Sample.CleanArchitecture.Infra.Data.EF;
using ApplicationUseCases = Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;
using Repository = Sample.CleanArchitecture.Infra.Data.EF.Repositories;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.GetProductAll;

[Collection(nameof(GetProductAllTestFixture))]
public class GetProductAllTest
{
    private readonly GetProductAllTestFixture _fixture;

    public GetProductAllTest(GetProductAllTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(SearchRetursListAndTotal))]
    [Trait("Integration/Application", "GetProductAll - Use Cases")]
    public async Task SearchRetursListAndTotal()
    {
        AppDbContext dbContext = _fixture.CreateDbContext(Guid.NewGuid());
        var exampleProductList = _fixture.GetExampleProductList(10);
        await dbContext.AddRangeAsync(exampleProductList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);       
        var useCase = new ApplicationUseCases.GetProductAll(repository);
        var input = new GetProductAllInput();
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(exampleProductList.Count);
        foreach (GetProductAllOutput outputItem in output)
        {
            var exampleItem = exampleProductList.Find(
                product => product.ProductId == outputItem.ProductId
            );
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
            outputItem.Price.Should().Be(exampleItem.Price);
        }
    }

    [Fact(DisplayName = nameof(SearchReturnsEmptyWhenEmpty))]
    [Trait("Integration/Application", "ListProduct - Use Cases")]
    public async Task SearchReturnsEmptyWhenEmpty()
    {
        AppDbContext dbContext = _fixture.CreateDbContext(Guid.NewGuid());
        var repository = new Repository.Repository<DomainEntity.Product>(dbContext);
        var useCase = new ApplicationUseCases.GetProductAll(repository);
        var input = new GetProductAllInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(0);
    }

}
