using System.Text.Json.Serialization;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;


public class GetProductAllOutput
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public IReadOnlyList<GetProductAllOutput> ProductAllOutput { get; set; } = default!;

    public static IReadOnlyList<GetProductAllOutput> FromProductList(IReadOnlyList<DomainEntity.Product> product)
    {
        var getProductAllListOutput = new List<GetProductAllOutput>();

        foreach (var item in product)
        {
            getProductAllListOutput.Add(new GetProductAllOutput
            {
                ProductId = item.ProductId,
                Name = item.Name,
                Description = item.Description,
                IsActive = item.IsActive,
                Price = item.Price,
                CreatedAt = item.CreatedAt
            });
        }
        return getProductAllListOutput;
    }
}
