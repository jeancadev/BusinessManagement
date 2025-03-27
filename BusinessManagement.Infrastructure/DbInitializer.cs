using BusinessManagement.Domain.Entities;
using BusinessManagement.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Seed(BusinessManagementDbContext context)
    {
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(new[]
            {
                new Customer ("Mario","Guzman","mario@mario.com"),
                new Customer ("Jorge","Perez","jorge@jorge.com")
            });
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(new[]
            {
                new Product("Laptop", "Dell", 1000m, 10),
                new Product("Smartphone", "Samsung", 800m, 20)
            });
            context.SaveChanges();
        }

        /*if (!context.Inventories.Any())
        {
            context.Inventories.AddRange(new[]
            {
                new Inventory (Guid.NewGuid(), 10),
                new Inventory (Guid.NewGuid(), 20)
            });
            context.SaveChanges();
        }*/

        /*if (!context.Sales.Any())
        {
            var newSale = new Sale(
                context.Customers.First().Id,
                new List<SaleItem>() // Lista vacía inicial de items
            );

            context.Sales.Add(newSale);
            context.SaveChanges();
        }*/
    }
}
