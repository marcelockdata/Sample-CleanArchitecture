using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;

public class GetProductOutput
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public GetProductOutput(
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
    }

    public static GetProductOutput FromProduct(DomainEntity.Product product)
       => new(
           product.ProductId,
           product.Name,
           product.Description,
           product.Price,
           product.IsActive,
           product.CreatedAt
       );
}
