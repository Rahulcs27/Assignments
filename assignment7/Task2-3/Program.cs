using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>
        {
            new Product(1, "Laptop", "Electronics", 50000),
            new Product(2, "Headphones", "Electronics", 1500),
            new Product(3, "Shirt", "Clothing", 800),
            new Product(4, "Mobile Phone", "Electronics", 20000),
            new Product(5, "Book", "Stationery", 500),
            new Product(6, "Refrigerator", "Electronics", 15000)
        };

        // Find Electronics Products Over ₹1000
        var expensiveElectronics = products
            .Where(p => p.Category == "Electronics" && p.Price > 1000)
            .ToList();

        Console.WriteLine("\nElectronics Products Over ₹1000:");
        foreach (var product in expensiveElectronics)
        {
            Console.WriteLine($"{product.Name} - Rs{product.Price}");
        }

        //  Step 2: Find Most Expensive Product using Max()
        double maxPrice = products.Max(p => p.Price);
        var mostExpensive = products.FirstOrDefault(p => p.Price == maxPrice);

        Console.WriteLine($"\n Most Expensive Product: {mostExpensive.Name} - Rs{mostExpensive.Price}");
    }
}
