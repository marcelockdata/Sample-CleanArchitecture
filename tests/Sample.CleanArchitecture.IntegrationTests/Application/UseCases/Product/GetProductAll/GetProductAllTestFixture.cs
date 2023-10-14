using Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.GetProductAll;

[CollectionDefinition(nameof(GetProductAllTestFixture))]
public class LGetProductAllTestFixtureCollection
    : ICollectionFixture<GetProductAllTestFixture>
{ }
public class GetProductAllTestFixture : ProductUseCasesBaseFixture
{
  
}
