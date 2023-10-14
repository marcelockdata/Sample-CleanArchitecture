using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;

public interface IUpdateProduct : IRequestHandler<UpdateProductInput, UpdateProductOutput> { }