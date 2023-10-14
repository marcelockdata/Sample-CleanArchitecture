using FluentValidation;

namespace Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;

public class GetProductInputValidator
 : AbstractValidator<GetProductInput>
{
    public GetProductInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}