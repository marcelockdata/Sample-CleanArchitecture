using FluentAssertions;
using Moq;
using Sample.CleanArchitecture.Application.Exceptions;
using Sample.CleanArchitecture.Infra.Data.EF;
using ApplicationUseCases = Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;


namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.GetProduct;

[Collection(nameof(GetProductTestFixture))]
public class GetProductTest
{
    private readonly GetProductTestFixture _fixture;

    public GetProductTest(GetProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetProduct))]
    [Trait("Application", "GetProduct - Use Cases")]
    public async Task GetProduct()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var exampleProduct = _fixture.GetExampleProduct();
        repositoryMock.Setup(x => x.GetById(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(exampleProduct);
        var input = new ApplicationUseCases.GetProductInput(exampleProduct.ProductId);
        var useCase = new ApplicationUseCases.GetProduct(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.GetById(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be(exampleProduct.Name);
        output.Price.Should().Be(exampleProduct.Price);
        output.Description.Should().Be(exampleProduct.Description);
        output.IsActive.Should().Be(exampleProduct.IsActive);
        output.ProductId.Should().Be(exampleProduct.ProductId);
        output.CreatedAt.Should().Be(exampleProduct.CreatedAt);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenProductDoesntExist))]
    [Trait("Application", "GetProduct - Use Cases")]
    public async Task NotFoundExceptionWhenProductDoesntExist()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var exampleGuid = Guid.NewGuid();
        repositoryMock.Setup(x => x.GetById(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(
            new NotFoundException($"Product '{exampleGuid}' not found")
        );
        var input = new ApplicationUseCases.GetProductInput(exampleGuid);
        var useCase = new ApplicationUseCases.GetProduct(repositoryMock.Object);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();
        repositoryMock.Verify(x => x.GetById(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}