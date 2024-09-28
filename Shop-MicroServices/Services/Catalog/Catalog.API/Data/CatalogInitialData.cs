using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetInitialProductList());

        await session.SaveChangesAsync();
    }


    private static IEnumerable<Product> GetInitialProductList()
    {
        var products = new List<Product>
    {
        new Product
        {
            Id = Guid.Parse("b334a1c9-6108-4a7d-9b56-d091b6f1a4ee"),
            Name = "IPhone X",
            Description = "This is the best iPhone yet.",
            Image = "product-1.png",
            Price = 950.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = Guid.Parse("a1c8d2e2-1ed5-4b71-85a5-2c7dc60c7b14"),
            Name = "Samsung Galaxy S20",
            Description = "High-performance smartphone with a stunning display.",
            Image = "product-2.png",
            Price = 900.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = Guid.Parse("e15df54e-8763-4b63-8b45-77e6ab8f4691"),
            Name = "Google Pixel 5",
            Description = "Google's flagship phone with an excellent camera.",
            Image = "product-3.png",
            Price = 850.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = Guid.Parse("742c5b73-b7a2-4cd2-a3b6-080d3c765aad"),
            Name = "OnePlus 8 Pro",
            Description = "Fast and fluid phone with powerful performance.",
            Image = "product-4.png",
            Price = 800.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = Guid.Parse("b872b0f7-233d-4417-8530-6267ef7a7b4a"),
            Name = "Sony Xperia 1 II",
            Description = "Sony's professional-grade phone for photography enthusiasts.",
            Image = "product-5.png",
            Price = 950.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product
        {
            Id = Guid.Parse("f02ffcc8-6b77-4d30-a70f-07f66fd19ed9"),
            Name = "Huawei P40 Pro",
            Description = "Premium phone with excellent camera capabilities.",
            Image = "product-6.png",
            Price = 890.00M,
            Category = new List<string> { "Smart Phone" }
        }
    };

        return products;
    }

}
