namespace Catalog.API.Products.GetProductById;


public record GetProductByIdResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;


internal class GetProductByIdHandler(IDocumentSession session) 
	: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
	public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
	{
		Product? product = await session.Query<Product>().Where(p=> p.Id == request.Id).SingleOrDefaultAsync(cancellationToken); 

		if (product is null) {

			throw new ProductNotFoundException(request.Id);
		}
		return new GetProductByIdResult(product);
	}
}
