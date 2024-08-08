namespace Catalog.API.Products.CreateProduct;


public record CreateProductRequest(
						 string Name,
						 string Description,
						 string Image,
						 decimal Price,
						 List<string> Category);

public record CreateProductResponse(Guid Id);
public class CreateProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/products", 
					async (CreateProductRequest request,ISender sender) =>
		{
			CreateProductCommand command = request.Adapt<CreateProductCommand>();

			var result = await sender.Send(command);

			CreateProductResponse response = result.Adapt<CreateProductResponse>();


			return Results.Created($"/products/{ response.Id}",response);
		})
		.WithName("CreateProduct")
		.Produces<CreateProductResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status404NotFound)
		.WithSummary("Create Product")
		.WithDescription("Create Product");
	}
}
