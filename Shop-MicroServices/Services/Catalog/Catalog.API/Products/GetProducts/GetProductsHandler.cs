
namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery() : IQuery<GetProductsResult>;
internal class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
	public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<Product> products = await session.Query<Product>().ToListAsync(token: cancellationToken);

		return new GetProductsResult(products);
	}

	//not being used anymore
	private static void LogProducts(ILogger<GetProductsHandler> logger, List<string?> productNameList)
	{
		logger.LogInformation($"Get Products Query Returned the Following List Of Products: {String.Join(",", productNameList)}");
	}
}
