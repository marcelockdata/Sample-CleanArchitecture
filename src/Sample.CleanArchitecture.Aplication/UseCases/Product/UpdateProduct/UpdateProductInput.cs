using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;

public class UpdateProductInput : IRequest<UpdateProductOutput>
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public UpdateProductInput(
        Guid productId,
        string name,
        string description,
        decimal price,
        bool isActive)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        IsActive = isActive;
    }
}
