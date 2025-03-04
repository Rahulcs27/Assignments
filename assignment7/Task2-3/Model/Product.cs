using System;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }

    public Product(int id, string name, string category, double price)
    {
        ProductID = id;
        Name = name;
        Category = category;
        Price = price;
    }
}
