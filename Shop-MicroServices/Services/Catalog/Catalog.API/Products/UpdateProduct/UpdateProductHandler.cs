

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResult(bool IsSuccess);
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
			throw new ProductNotFoundException();
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
