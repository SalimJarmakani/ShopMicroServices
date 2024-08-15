
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsDeleted);
public class DeleteProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/products/{id}", async (Guid id,ISender sender) =>
		{
			try
			{
				DeleteProductCommand deleteCommand  = new DeleteProductCommand(id);

				var result = await sender.Send(deleteCommand);

				DeleteProductResponse response = new DeleteProductResponse(result.IsDeleted);

				return Results.Ok(response);

			}
			catch (ProductNotFoundException ex) {
				return Results.BadRequest(ex.Message);
			}
		})
		.WithName("DeletProduct")
		.Produces(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithDescription("Deletes Products With Given Id");
	}
}
