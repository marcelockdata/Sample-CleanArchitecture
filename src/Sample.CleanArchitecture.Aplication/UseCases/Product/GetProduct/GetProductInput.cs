using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;

public class GetProductInput : IRequest<GetProductOutput>
{
    public GetProductInput(Guid id)
      => Id = id;

    public Guid Id { get; set; }
}
