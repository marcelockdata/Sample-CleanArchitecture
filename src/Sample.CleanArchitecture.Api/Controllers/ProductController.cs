using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.CleanArchitecture.Api.ApiModels;
using Sample.CleanArchitecture.Api.ApiModels.Product;
using Sample.CleanArchitecture.Application.UseCases.Product.CreateProduct;
using Sample.CleanArchitecture.Application.UseCases.Product.DeleteProduct;
using Sample.CleanArchitecture.Application.UseCases.Product.GetProduct;
using Sample.CleanArchitecture.Application.UseCases.Product.GetProductAll;
using Sample.CleanArchitecture.Application.UseCases.Product.UpdateProduct;

namespace Sample.CleanArchitecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
         => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateProductOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateProductInput input, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create),
            new { output.ProductId },
            new ApiResponse<CreateProductOutput>(output));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<UpdateProductOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateProductApiInput apiInput,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var input = new UpdateProductInput(
            id,
            apiInput.Name,
            apiInput.Description,
            apiInput.Price,
            apiInput.IsActive
        );
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<UpdateProductOutput>(output));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProductInput(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetProductOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetProductInput(id), cancellationToken);
        return Ok(new ApiResponse<GetProductOutput>(output));
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ApiResponseList<GetProductAllOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetProductAllInput(), cancellationToken);
        return Ok(new ApiResponseList<GetProductAllOutput>(output));
    }
}

