
namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery() : IQuery<GetProductsResult>;
internal class GetProductsHandler(IDocumentSession session,ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
	public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<Product> products = await session.Query<Product>().ToListAsync(token: cancellationToken);

		var productNameList = products.Select(products => products.Name).ToList();

		LogProducts(logger, productNameList);

		return new GetProductsResult(products);
	}

	private static void LogProducts(ILogger<GetProductsHandler> logger, List<string?> productNameList)
	{
		logger.LogInformation($"Get Products Query Returned the Following List Of Products: {String.Join(",", productNameList)}");
	}
}
