
namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> products);
public class GetProductsEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products", async([AsParameters] GetProductsRequest request,ISender sender) =>
		{

			var query = request.Adapt<GetProductsQuery>();

			var result = await sender.Send(query);

			GetProductsResponse response = result.Adapt<GetProductsResponse>();

			return Results.Ok(response);
			
		}).WithName("GetProducts")
		.WithDescription("Gets All Products")
		.ProducesProblem(StatusCodes.Status400BadRequest);
	}
}
