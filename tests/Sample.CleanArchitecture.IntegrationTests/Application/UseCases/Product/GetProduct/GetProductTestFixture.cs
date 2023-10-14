using Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.Common;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.GetProduct;


[CollectionDefinition(nameof(GetProductTestFixture))]
public class GetProductTestFixtureCollection
    : ICollectionFixture<GetProductTestFixture>
{ }

public class GetProductTestFixture
    : ProductUseCasesBaseFixture
{
}