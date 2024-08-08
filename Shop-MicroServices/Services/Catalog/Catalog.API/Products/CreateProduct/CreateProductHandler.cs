namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
						 string Name,
					     string Description,
						 string Image,
						 decimal Price,
						 List<string> Category) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		Product product = MapDataToProduct(command);


		session.Store(product);

		await session.SaveChangesAsync(cancellationToken);


		return new CreateProductResult(product.Id);
	}


	private static Product MapDataToProduct(CreateProductCommand command)
	{
		Product product = new Product {
		
			Name = command.Name,
			Description = command.Description,
			Image = command.Image,
			Price = command.Price,
			Category = command.Category,
		};

		return product;
	}
}
