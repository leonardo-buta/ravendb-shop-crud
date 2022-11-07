using Raven.Client.Documents.Session;
using Shop.Models;
using Shop.Raven;

//CreateProduct("Maçã", 5.15M);
//CreateProduct("Banana", 3.05M);
//CreateProduct("Café", 11.35M);

//GetProduct("06eb1b79-4f8c-46f3-b306-ffb779752fe3");
//GetAllProducts();
//GetAllPagedProducts(1, 2);
//CreateCart("test@test.com");
//AddProductToCart("test@test.com", "d47b952f-6880-42f9-af81-4773e4fca65d", 3);
AddProductToCart("test@test.com", "a1c38ee1-0574-4628-b633-879e61f43201", 1);

static void CreateProduct(string name, decimal price)
{
    var p1 = new Product(Guid.NewGuid().ToString(), name, price);

    using var session = DocumentStoreHolder.Store.OpenSession();
    session.Store(p1);
    session.SaveChanges();
}

static void GetProduct(string id)
{
    using var session = DocumentStoreHolder.Store.OpenSession();
    var p = session.Load<Product>(id);
    Console.WriteLine($"Product: {p.Name}{Environment.NewLine}Price: {p.Price}");
}

static void GetAllProducts()
{
    using var session = DocumentStoreHolder.Store.OpenSession();
    var products = session.Query<Product>().ToList();
    products.ForEach(p => Console.WriteLine($"Product: {p.Name}{Environment.NewLine}Price: {p.Price}{Environment.NewLine}"));
}

static void GetAllPagedProducts(int pageIndex, int pageSize)
{
    int skip = (pageIndex - 1) * pageSize;
    int take = pageSize;

    using var session = DocumentStoreHolder.Store.OpenSession();
    var products = session.Query<Product>()
        .Statistics(out QueryStatistics stats)
        .Skip(skip)
        .Take(take)
        .ToList();

    Console.WriteLine($"Showing results {skip} to {skip + pageSize} of {stats.TotalResults}");

    products.ForEach(p => Console.WriteLine($"Product: {p.Name}{Environment.NewLine}Price: {p.Price}{Environment.NewLine}"));

    Console.WriteLine($"Took {stats.DurationInMs} ms");
}

static void CreateCart(string customer)
{
    var cart = new Cart(Guid.NewGuid().ToString(), customer);
    
    using var session = DocumentStoreHolder.Store.OpenSession();
    session.Store(cart);
    session.SaveChanges();
}

static void AddProductToCart(string customer, string productId, int quantity)
{
    using var session = DocumentStoreHolder.Store.OpenSession();
    var cart = session.Query<Cart>().SingleOrDefault(x => x.Customer == customer);
    var product = session.Load<Product>(productId);

    if (cart != null && product != null)
    {
        cart.Lines.Add(new CartLine(product.Name, product.Price, quantity));
        session.SaveChanges();
    }
}