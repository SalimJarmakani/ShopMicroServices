
using Catalog.API.Products.GetProducts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.GetProductById;


public record GetProductByIdResponse(Product Product);


public class GetProductByIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
		{
			try
			{
				if (id == Guid.Empty)
				{
					return Results.Empty;
				}

				var result = await sender.Send(new GetProductByIdQuery(id));

				GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();

				return Results.Ok(response);
			}
			catch (ProductNotFoundException ex) {

				return Results.BadRequest(ex.Message);
			
			}
		})
		.WithName("GetProductById")
		.WithDescription("Gets a product by id")
		.Produces(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest);
		;
	}
}
