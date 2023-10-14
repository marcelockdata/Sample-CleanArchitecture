using FluentAssertions;
using FluentValidation;
using Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;

namespace Sample.CleanArchitecture.IntegrationTests.Application.UseCases.Product.GetProduct;

[Collection(nameof(GetProductTestFixture))]
public class GetProductInputValidatorTest
{
    private readonly GetProductTestFixture _fixture;

    public GetProductInputValidatorTest(GetProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("Application", "GetProductInputValidation - UseCases")]
    public void ValidationOk()
    {
        var validInput = new GetProductInput(Guid.NewGuid());
        var validator = new GetProductInputValidator();

        var validationResult = validator.Validate(validInput);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(InvalidWhenEmptyGuidId))]
    [Trait("Application", "GetProductInputValidation - UseCases")]
    public void InvalidWhenEmptyGuidId()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        var invalidInput = new GetProductInput(Guid.Empty);
        var validator = new GetProductInputValidator();

        var validationResult = validator.Validate(invalidInput);

        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].ErrorMessage
            .Should().Be("'Id' must not be empty.");
    }
}

