namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.UpdateProduct;

public class UpdateProductTestDataGenerator
{
    public static IEnumerable<object[]> GetProductsToUpdate(int times = 5)
    {
        var fixture = new UpdateProductTestFixture();
        for (int indice = 0; indice < times; indice++)
        {
            var exampleProduct = fixture.GetExampleProduct();
            var exampleInput = fixture.GetValidInput(exampleProduct.ProductId);
            yield return new object[] {
                exampleProduct, exampleInput
            };
        }
    }

    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new UpdateProductTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at least 3 characters long"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should be less or equal 100 characters long"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Description should be less or equal 255 characters long"
                    });
                    break;
                default:
                    break;
            }
        }

        return invalidInputsList;
    }
}

