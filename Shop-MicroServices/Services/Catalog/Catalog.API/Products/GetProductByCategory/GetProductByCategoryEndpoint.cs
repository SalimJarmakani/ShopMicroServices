
using Microsoft.AspNetCore.Http.HttpResults;
using OpenTelemetry.Trace;

namespace Catalog.API.Products.GetProductByCategory;


public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
		{

			var result = await sender.Send(new GetProductByCategoryQuery(category));

			GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();

			return Results.Ok(response);

		})
		.WithDescription("Gets a List of products based on the category they belong to")
		.WithName("GetProductByCategory")
		.Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Product By Category");
	}
}
