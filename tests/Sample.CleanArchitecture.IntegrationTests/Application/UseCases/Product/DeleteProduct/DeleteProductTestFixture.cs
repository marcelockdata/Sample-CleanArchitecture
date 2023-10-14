using Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.Common;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.DeleteProduct;


[CollectionDefinition(nameof(DeleteProductTestFixture))]
public class DeleteProductTestFixtureCollection
    : ICollectionFixture<DeleteProductTestFixture>
{ }

public class DeleteProductTestFixture
    : ProductUseCasesBaseFixture
{
}

