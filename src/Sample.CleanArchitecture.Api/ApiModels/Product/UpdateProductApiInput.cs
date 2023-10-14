namespace Sample.CleanArchitecture.Api.ApiModels.Product;

public class UpdateProductApiInput
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public UpdateProductApiInput(
        string name,
        string description,
        decimal price,
        bool isActive)
    {
        Name = name;
        Description = description;
        Price = price;
        IsActive = isActive;
    }
}
