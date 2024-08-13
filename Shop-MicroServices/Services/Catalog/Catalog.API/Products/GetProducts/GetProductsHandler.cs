
namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery() : IQuery<GetProductsResult>;
internal class GetProductsHandler(IDocumentSession session,ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
	public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		IEnumerable<Product> products = await session.Query<Product>().ToListAsync(token: cancellationToken);

		return new GetProductsResult(products);
	}
}
