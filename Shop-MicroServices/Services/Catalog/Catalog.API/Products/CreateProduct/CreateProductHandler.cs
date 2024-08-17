namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
						 string Name,
						 string Description,
						 string Image,
						 decimal Price,
						 List<string> Category) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{

	public CreateProductCommandValidator()
	{

		RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required");
		RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");
		RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is Required");
		RuleFor(x => x.Image).NotEmpty().WithMessage("Image Is Required");
		RuleFor(x => x.Price).NotEmpty().WithMessage("Price Is Required");



	}
}
internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
	: ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{

		var valid = await validator.ValidateAsync(command, cancellationToken);

		var erros = valid.Errors.Select(e => e.ErrorMessage).ToList();

		var hasErrors = valid.Errors.Any();

		if (hasErrors)
		{
			throw new ValidationException(erros.FirstOrDefault()!);
		}
		Product product = MapDataToProduct(command);


		session.Store(product);

		await session.SaveChangesAsync(cancellationToken);


		return new CreateProductResult(product.Id);
	}


	private static Product MapDataToProduct(CreateProductCommand command)
	{
		Product product = new Product
		{

			Name = command.Name,
			Description = command.Description,
			Image = command.Image,
			Price = command.Price,
			Category = command.Category,
		};

		return product;
	}
}

