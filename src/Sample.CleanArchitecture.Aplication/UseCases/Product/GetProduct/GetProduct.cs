using Sample.CleanArchitecture.Application.Exceptions;
using Sample.CleanArchitecture.Application.Interfaces;
using Sample.CleanArchitecture.Domain.Interfaces;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;

public class GetProduct : IGetProduct
{
    private readonly IRepository<DomainEntity.Product> _repository;

    public GetProduct(IRepository<DomainEntity.Product> repository)
        => (_repository) = (repository);
    public async Task<GetProductOutput> Handle(GetProductInput input, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(input.Id, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Produto '{input.Id}' not found.");
        return GetProductOutput.FromProduct(product!);
    }
}
