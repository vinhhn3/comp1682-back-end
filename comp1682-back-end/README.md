## Set up the Data Model
In the Solution Explorer, right-click on your project and choose Add > New Folder. Name the folder "Models."

Inside the "Models" folder, create two classes: "Product.cs" and "Category.cs."

```cs
// Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

// Category.cs
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

```

