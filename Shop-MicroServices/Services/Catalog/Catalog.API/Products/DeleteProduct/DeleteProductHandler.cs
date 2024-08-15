
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResult(bool IsDeleted);
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

internal class DeleteProductHandler(IDocumentSession session,ILogger<DeleteProductHandler> logger) 
	
	: ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
	public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		Product? product = await session.Query<Product>()
			.Where(p => p.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);

		if (product is null) {
			throw new ProductNotFoundException();
		}

		session.Delete(product);

		await session.SaveChangesAsync(cancellationToken);


		logger.LogInformation($"Deleted Product With Id: {product.Id} Successfully");


		return new DeleteProductResult(true);
	}
}
