using Sample.CleanArchitecture.Application.Exceptions;
using Sample.CleanArchitecture.Application.Interfaces;
using Sample.CleanArchitecture.Domain.Interfaces;
using DomainEntity = Sample.CleanArchitecture.Domain.Entities;

namespace Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;

public class UpdateProduct : IUpdateProduct
{
    private readonly IRepository<DomainEntity.Product> _repository;
    private readonly IUnitOfWork _uinitOfWork;

    public UpdateProduct(
        IRepository<DomainEntity.Product> repository,
        IUnitOfWork uinitOfWork)
        => (_uinitOfWork, _repository)
            = (uinitOfWork, repository);

    public async Task<UpdateProductOutput> Handle(UpdateProductInput input, CancellationToken cancellationToken)
    {
        var product = await _repository.GetById(input.ProductId, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Produto '{input.ProductId}' not found.");
        product!.Update(input.Name, input.Description, input.Price);

        if (input.IsActive!) product.Activate();
        else product.Deactivate();

        await _repository.Update(product);
        await _uinitOfWork.Commit(cancellationToken);
        return UpdateProductOutput.FromProduct(product);
    }
}
