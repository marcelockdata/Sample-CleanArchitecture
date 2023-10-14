using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;

public interface IGetProduct : IRequestHandler<GetProductInput, GetProductOutput> {}
