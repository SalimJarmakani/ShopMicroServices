
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<Product> Products);
public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;


internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
	: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
	public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
	{
		List<Product> products = (List<Product>)await 
			session.Query<Product>()
			.Where(p=> p.Category.Contains(request.Category))
			.ToListAsync(cancellationToken);

		logger.LogInformation($"Products with category {request.Category} are returned");

		return new GetProductByCategoryResult(products);
	}
}
