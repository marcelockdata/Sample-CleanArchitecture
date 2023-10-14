using MediatR;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;

public class GetProductAllInput : IRequest<IReadOnlyList<GetProductAllOutput>> {}
