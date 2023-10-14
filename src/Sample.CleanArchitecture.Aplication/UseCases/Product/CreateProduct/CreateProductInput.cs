using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;

public class CreateProductInput : IRequest<CreateProductOutput>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public CreateProductInput(
        string name,
        string description,
        decimal price,
        bool isActive)
    {
        Name = name;
        Description = description;
        Price = price;
        IsActive = 
        IsActive = isActive;
    }
}
