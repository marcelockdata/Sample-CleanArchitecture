using Bogus;
using Sample.CleanArchitecture.UnitTests.Common;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.UnitTests.Domain.Entity.Product;


[CollectionDefinition(nameof(ProductTestFixture))]
public class ProductTestFixtureCollection
    : ICollectionFixture<ProductTestFixture>
{ }

public class ProductTestFixture : BaseFixture
{
    public ProductTestFixture()
        : base() { }

    public string GetValidProductName()
    {
        var productName = "";
        while (productName.Length < 3)
            productName = Faker.Commerce.ProductName();
        if (productName.Length > 100)
            productName = productName[..100];
        return productName;
    }

    private string GetValidProductDescription()
    {
        var productDescription =
            Faker.Commerce.ProductDescription();
        if (productDescription.Length > 255)
            productDescription =
                productDescription[..255];
        return productDescription;
    }

    private decimal GetValidProductPrice()
    {
        return Convert.ToDecimal(Faker.Commerce.Price());
    }

    public DomainEntity.Product GetValidProduct()
        => new(
            Guid.NewGuid(),
            GetValidProductName(),
            GetValidProductDescription(),
            GetValidProductPrice(),
            true,
            DateTime.Now
        );


    public DomainEntity.Product GetInValidProduct()
       => new(
           Guid.NewGuid(),
           GetValidProductName(),
           GetValidProductDescription(),
           GetValidProductPrice(),
           false,
           DateTime.Now
       );
}