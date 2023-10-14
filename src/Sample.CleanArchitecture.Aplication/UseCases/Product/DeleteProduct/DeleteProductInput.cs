using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.DeleteProduct;

public class DeleteProductInput : IRequest<Unit>
{
    public DeleteProductInput(Guid id)
        => Id = id;

    public Guid Id { get; set; }
}
