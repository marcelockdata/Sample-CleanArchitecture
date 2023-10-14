using Sample.CleanArchitecture.Domain.Interfaces;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;

public class GetProductAll : IGetProductAll
{
    private readonly IRepository<DomainEntity.Product> _repository;

    public GetProductAll(IRepository<DomainEntity.Product> repository)
        => (_repository) = (repository);

    public async Task<IReadOnlyList<GetProductAllOutput>> Handle(GetProductAllInput request, CancellationToken cancellationToken)
    {
        var productList = await _repository.GetAll();

        return GetProductAllOutput.FromProductList(productList);
    }
}
