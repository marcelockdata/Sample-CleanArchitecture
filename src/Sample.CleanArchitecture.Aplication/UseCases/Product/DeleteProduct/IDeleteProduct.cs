using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.DeleteProduct;

public interface IDeleteProduct
    : IRequestHandler<DeleteProductInput, Unit>
{ }
