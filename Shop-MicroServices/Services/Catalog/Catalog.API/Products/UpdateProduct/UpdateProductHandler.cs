namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResult(bool IsSuccess);



public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{

	public UpdateProductCommandValidator()
	{

		RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id Is Required");

		RuleFor(command => command.Name).NotEmpty().WithMessage("Product Name is Required")
		.Length(2,150).WithMessage("Name must have between 2 and 150 characters");

		RuleFor(command => command.Price)
			.GreaterThan(0)
			.WithMessage("Price Must Be Greater Than 0");
	}
}
public record UpdateProductCommand(Guid Id,string Name,string Description,List<string> Category,string Image,decimal Price)
	: ICommand<UpdateProductResult> ;
internal class UpdateProductHandler(IDocumentSession session,ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		Product? product = await session.Query<Product>()
			.Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);

		if (product is null)
		{
			throw new ProductNotFoundException(request.Id);
		}

		product.Name = request.Name;
		product.Description = request.Description;
		product.Category = request.Category;
		product.Image = request.Image;
		product.Price = request.Price;

		session.Update(product);

		await session.SaveChangesAsync(cancellationToken);

		logger.LogInformation($"Updated Product With Id: {product.Id} Successfully");

		return new UpdateProductResult(true);


	}
}
