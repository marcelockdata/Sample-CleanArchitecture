using Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;
using Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.Common;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.UpdateProduct;


[CollectionDefinition(nameof(UpdateProductTestFixture))]
public class UpdateProductTestFixtureCollection
    : ICollectionFixture<UpdateProductTestFixture>
{ }
public class UpdateProductTestFixture : ProductUseCasesBaseFixture
{
    public UpdateProductInput GetValidInput(Guid? id = null)
    {
        var product = GetValidProduct();
        return new UpdateProductInput(
            id ?? Guid.NewGuid(),
            GetValidProductName(),
            GetValidProductDescription(),
            GetValidProductPrice(),
            GetRandomBoolean()
        );
    }

    public UpdateProductInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }

    public UpdateProductInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();
        var tooLongNameForProduct = Faker.Commerce.ProductName();
        while (tooLongNameForProduct.Length <= 100)
            tooLongNameForProduct = $"{tooLongNameForProduct} {Faker.Commerce.ProductName()}";
        invalidInputTooLongName.Name = tooLongNameForProduct;
        return invalidInputTooLongName;
    }

    public UpdateProductInput GetInvalidInputTooLongDescription()
    {
        var invalidInputTooLongDescription = GetValidInput();
        var tooLongDescriptionForProduct = Faker.Commerce.ProductDescription();
        while (tooLongDescriptionForProduct.Length <= 255)
            tooLongDescriptionForProduct = $"{tooLongDescriptionForProduct} {Faker.Commerce.ProductDescription()}";
        invalidInputTooLongDescription.Description = tooLongDescriptionForProduct;
        return invalidInputTooLongDescription;
    }

}
