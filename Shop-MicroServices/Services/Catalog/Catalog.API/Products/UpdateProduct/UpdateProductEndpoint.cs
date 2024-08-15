
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, string Description, List<string> Category, string Image, decimal Price);

public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/products/Update", async (ISender sender, UpdateProductRequest request) =>
		{
			try
			{
				var updateCommand = request.Adapt<UpdateProductCommand>();

				var result = await sender.Send(updateCommand);

				return Results.Ok(new UpdateProductResponse(result.IsSuccess));
			}
			catch (ProductNotFoundException e) {
				
				return Results.BadRequest(e.Message);
			}
		});
	}
}
