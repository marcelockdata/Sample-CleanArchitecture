using Sample.CleanArchitecture.Domain.Validation;

namespace Sample.CleanArchitecture.Domain.Entities;

public class Product : IEntidade
{
    public Product(
        Guid productId,
        string name,
        string description,
        decimal price,
        bool isActive,
        DateTime createdAt)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        IsActive = isActive;
        CreatedAt = createdAt;

        Validate();
    }

    public Guid ProductId { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }
    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(
        string name,
        string description,
        decimal price)
    {
        Name = name;
        Description = description ?? Description;
        Price = price;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MaxLength(Name, 100, nameof(Name));

        DomainValidation.NotNull(Description, nameof(Description));
        DomainValidation.MaxLength(Description, 255, nameof(Description));
    }
}
