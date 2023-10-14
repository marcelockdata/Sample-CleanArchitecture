using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;

public interface IGetProductAll : IRequestHandler<GetProductAllInput, IReadOnlyList<GetProductAllOutput>> { }
