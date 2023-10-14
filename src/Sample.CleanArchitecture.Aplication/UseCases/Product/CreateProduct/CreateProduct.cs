using Sample.CleanArchitecture.Application.Interfaces;
using Sample.CleanArchitecture.Domain.Interfaces;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;

public class CreateProduct : ICreateProduct
{
    private readonly IRepository<DomainEntity.Product> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProduct(IRepository<DomainEntity.Product> repository, IUnitOfWork unitOfWork)
        => (_repository, _unitOfWork) = (repository, unitOfWork);

    public async Task<CreateProductOutput> Handle(CreateProductInput input, CancellationToken cancellationToken)
    {
        var product = new DomainEntity.Product(
            Guid.NewGuid(),
            input.Name,
            input.Description,
            input.Price,
            input.IsActive,
            DateTime.Now
         );

        await _repository.Add(product);
        await _unitOfWork.Commit(cancellationToken);

        return await Task.FromResult(CreateProductOutput.FromProduct(product));
    }
}
