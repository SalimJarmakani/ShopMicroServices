using Core.Services.Exceptions;

namespace Catalog.API.Execptions;

public class ProductNotFoundException : NotFoundException
{
	public ProductNotFoundException(Guid id) : base("Product",id) { }
}
