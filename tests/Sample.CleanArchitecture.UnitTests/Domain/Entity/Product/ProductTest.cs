using FluentAssertions;
using Sample.CleanArchitecture.Domain.Exceptions;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.UnitTests.Domain.Entity.Product;

[Collection(nameof(ProductTestFixture))]
public class ProductTest
{
    private readonly ProductTestFixture _productTestFixture;

    public ProductTest(ProductTestFixture productTestFixture)
        => _productTestFixture = productTestFixture;


    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Product")]
    public void Instantiate()
    {
        var validProduct = _productTestFixture.GetValidProduct();
        var datetimeBefore = DateTime.Now;

        var product = new DomainEntity.Product(
            validProduct.ProductId,
            validProduct.Name,
            validProduct.Description,
            validProduct.Price,
            validProduct.IsActive,
            validProduct.CreatedAt);

        var datetimeAfter = DateTime.Now.AddSeconds(1);

        product.Should().NotBeNull();
        product.ProductId.Should().NotBeEmpty();
        product.Name.Should().Be(validProduct.Name);
        product.Description.Should().Be(validProduct.Description);
        product.Price.Should().Be(validProduct.Price);
        product.IsActive.Should().Be(validProduct.IsActive);
        product.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (product.IsActive).Should().BeTrue();
        (product.CreatedAt <= datetimeAfter).Should().BeTrue();
        (product.CreatedAt >= datetimeBefore).Should().BeFalse();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Product")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var validProduct = _productTestFixture.GetValidProduct();
        var datetimeBefore = DateTime.Now;

        var product = new DomainEntity.Product(
            validProduct.ProductId,
            validProduct.Name,
            validProduct.Description,
            validProduct.Price,
            isActive,
            validProduct.CreatedAt);

        var datetimeAfter = DateTime.Now.AddSeconds(1);

        product.Should().NotBeNull();
        product.ProductId.Should().NotBeEmpty();
        product.Name.Should().Be(validProduct.Name);
        product.Description.Should().Be(validProduct.Description);
        product.Price.Should().Be(validProduct.Price);
        product.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (product.IsActive).Should().Be(isActive);
        (product.CreatedAt <= datetimeAfter).Should().BeTrue();
        (product.CreatedAt >= datetimeBefore).Should().BeFalse();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Product")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        var validProduct = _productTestFixture.GetValidProduct();

        Action action =
            () => new DomainEntity.Product(
                validProduct.ProductId,
                name!,
                validProduct.Description,
                validProduct.Price,
                validProduct.IsActive,
                validProduct.CreatedAt);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Product")]
    [InlineData(null)]
    public void InstantiateErrorWhenDescriptionIsNull(string? description)
    {
        var validProduct = _productTestFixture.GetValidProduct();

        Action action =
            () => new DomainEntity.Product(
                validProduct.ProductId,
                validProduct.Name,
                description!,
                validProduct.Price,
                validProduct.IsActive,
                validProduct.CreatedAt);


        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Product")]
    [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var validProduct = _productTestFixture.GetValidProduct();

        Action action =
            () => new DomainEntity.Product(
                validProduct.ProductId,
                invalidName,
                validProduct.Description,
                validProduct.Price,
                validProduct.IsActive,
                validProduct.CreatedAt);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
    {
        var fixture = new ProductTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return new object[] {
                fixture.GetValidProductName()[..(isOdd ? 1 : 2)]
            };
        }
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan100Characters))]
    [Trait("Domain", "Product")]
    public void InstantiateErrorWhenNameIsGreaterThan100Characters()
    {
        var validProduct = _productTestFixture.GetValidProduct();
        var invalidName = String.Join(null, Enumerable.Range(1, 101).Select(_ => "a").ToArray());

        Action action =
             () => new DomainEntity.Product(
                 validProduct.ProductId,
                 invalidName,
                 validProduct.Description,
                 validProduct.Price,
                 validProduct.IsActive,
                 validProduct.CreatedAt);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal 100 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan255Characters))]
    [Trait("Domain", "Product")]
    public void InstantiateErrorWhenDescriptionIsGreaterThan255Characters()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
        var validProduct = _productTestFixture.GetValidProduct();

        Action action =
            () => new DomainEntity.Product(
                validProduct.ProductId,
                validProduct.Name,
                invalidDescription,
                validProduct.Price,
                validProduct.IsActive,
                validProduct.CreatedAt);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should be less or equal 255 characters long");
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Product")]
    public void Activate()
    {
        var validProduct = _productTestFixture.GetValidProduct();

        var product = new DomainEntity.Product(
                validProduct.ProductId,
                validProduct.Name,
                validProduct.Description,
                validProduct.Price,
                false,
                validProduct.CreatedAt);

        product.Activate();

        product.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Product")]
    public void Deactivate()
    {
        var validProduct = _productTestFixture.GetValidProduct();

        var product = new DomainEntity.Product(
                validProduct.ProductId,
                validProduct.Name,
                validProduct.Description,
                validProduct.Price,
                true,
                validProduct.CreatedAt);

        product.Deactivate();

        product.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Product")]
    public void Update()
    {
        var product = _productTestFixture.GetValidProduct();
        var productWithNewValues = _productTestFixture.GetValidProduct();

        product.Update(
            productWithNewValues.Name,
            productWithNewValues.Description,
            productWithNewValues.Price);


        product.Name.Should().Be(productWithNewValues.Name);
        product.Description.Should().Be(productWithNewValues.Description);
        product.Price.Should().Be(productWithNewValues.Price);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Product")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var product = _productTestFixture.GetValidProduct();

        Action action =
            () => product.Update(
            name!,
            product.Description,
            product.Price);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Product")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ca")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var product = _productTestFixture.GetValidProduct();

        Action action =
           () => product.Update(
           invalidName!,
           product.Description,
           product.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan100Characters))]
    [Trait("Domain", "Product")]
    public void UpdateErrorWhenNameIsGreaterThan100Characters()
    {
        var product = _productTestFixture.GetValidProduct();
        var invalidName = _productTestFixture.Faker.Lorem.Letter(101);

        Action action =
           () => product.Update(
           invalidName!,
           product.Description,
           product.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal 100 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan255Characters))]
    [Trait("Domain", "Product")]
    public void UpdateErrorWhenDescriptionIsGreaterThan255Characters()
    {
        var product = _productTestFixture.GetValidProduct();
        var invalidDescription =
            _productTestFixture.Faker.Commerce.ProductDescription();
        while (invalidDescription.Length <= 255)
            invalidDescription = $"{invalidDescription} {_productTestFixture.Faker.Commerce.ProductDescription()}";

        Action action =
             () => product.Update(
             product.Name,
             invalidDescription,
             product.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should be less or equal 255 characters long");
    }
}
