using Sample.CleanArchitecture.IntegrationTests.Base;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;


namespace Sample.CleanArchitecture.IntegrationTests.Infra.Data.EF.Repositories;

[CollectionDefinition(nameof(ProductRepositoryTestFixture))]
public class ProductRepositoryTestFixtureCollection
    : ICollectionFixture<ProductRepositoryTestFixture>
{ }
public class ProductRepositoryTestFixture : BaseFixture
{
    public string GetValidProductName()
    {
        var productName = "";
        while (productName.Length < 3)
            productName = Faker.Commerce.ProductName();
        if (productName.Length > 100)
            productName = productName[..100];
        return productName;
    }

    public string GetValidProductDescription()
    {
        var productDescription =
            Faker.Commerce.ProductDescription();
        if (productDescription.Length > 255)
            productDescription =
                productDescription[..255];
        return productDescription;
    }

    public decimal GetValidProductPrice()
    {
        return Convert.ToDecimal(Faker.Commerce.Price());
    }

    public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public DomainEntity.Product GetExampleProduct()
        => new(
            Guid.NewGuid(),
            GetValidProductName(),
            GetValidProductDescription(),
            GetValidProductPrice(),
            true,
            DateTime.Now
        );

    public List<DomainEntity.Product> GetExampleProductList(int length = 5)
      => Enumerable.Range(1, length)
          .Select(_ => GetExampleProduct()).ToList();
}
