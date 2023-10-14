using Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;
using Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.Common;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.CreateProduct;

[CollectionDefinition(nameof(CreateProductTestFixture))]
public class CreateProductTestFixtureCollection
    : ICollectionFixture<CreateProductTestFixture>
{ }
public class CreateProductTestFixture : ProductUseCasesBaseFixture
{
    public CreateProductInput GetInput()
    {
        var product = GetValidProduct();
        return new CreateProductInput(
            product.Name,
            product.Description,
            product.Price,
            product.IsActive
        );
    }

    public CreateProductInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetInput();
        var tooLongNameForProduct = Faker.Commerce.ProductName();
        while (tooLongNameForProduct.Length <= 100)
            tooLongNameForProduct = $"{tooLongNameForProduct} {Faker.Commerce.ProductName()}";
        invalidInputTooLongName.Name = tooLongNameForProduct;
        return invalidInputTooLongName;
    }

    public CreateProductInput GetInvalidInputProductNull()
    {
        var invalidInputDescriptionNull = GetInput();
        invalidInputDescriptionNull.Description = null!;
        return invalidInputDescriptionNull;
    }

    public CreateProductInput GetInvalidInputTooLongDescription()
    {
        var invalidInputTooLongDescription = GetInput();
        var tooLongDescriptionForProduct = Faker.Commerce.ProductDescription();
        while (tooLongDescriptionForProduct.Length <= 255)
            tooLongDescriptionForProduct = $"{tooLongDescriptionForProduct} {Faker.Commerce.ProductDescription()}";
        invalidInputTooLongDescription.Description = tooLongDescriptionForProduct;
        return invalidInputTooLongDescription;
    }

    public CreateProductInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }
}
