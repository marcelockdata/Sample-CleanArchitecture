using Sample.CleanArchitecture.Application.Interfaces;
using Sample.CleanArchitecture.Domain.Interfaces;
using Sample.CleanArchitecture.IntegrationTests.Base;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;
using Moq;
using Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.Common;

public class ProductUseCasesBaseFixture : BaseFixture
{

    public Mock<IRepository<DomainEntity.Product>> GetRepositoryMock()
    => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();

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

    public DomainEntity.Product GetValidProduct()
        => new(
            Guid.NewGuid(),
            GetValidProductName(),
            GetValidProductDescription(),
            GetValidProductPrice(),
            GetRandomBoolean(),
            DateTime.Now
        );


    public DomainEntity.Product GetExampleProduct()
       => new(
           Guid.NewGuid(),
           GetValidProductName(),
           GetValidProductDescription(),
           GetValidProductPrice(),
           GetRandomBoolean(),
           DateTime.Now
       );

    public List<DomainEntity.Product> GetExampleProductList(int length = 5)
    => Enumerable.Range(1, length)
        .Select(_ => GetExampleProduct()).ToList();
}
