using MediatR;
using Sample.CleanArchitecture.Application.Exceptions;
using Sample.CleanArchitecture.Application.Interfaces;
using Sample.CleanArchitecture.Domain.Interfaces;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.DeleteProduct;

public class DeleteProduct : IDeleteProduct
{ 
    private readonly IRepository<DomainEntity.Product> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProduct(IRepository<DomainEntity.Product> repository, IUnitOfWork unitOfWork)
        => (_repository, _unitOfWork) = (repository, unitOfWork);

    public async Task<Unit> Handle(DeleteProductInput input, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(input.Id, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Product '{input.Id}' not found.");
        await _repository.Delete(product!);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}