using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;

public interface ICreateProduct : IRequestHandler<CreateProductInput, CreateProductOutput> { }
