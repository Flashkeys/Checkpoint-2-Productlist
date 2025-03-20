
ProduktList productList = new ProduktList();
productList.AddProduct(new Produkt("El", "Phone", 699));
productList.AddProduct(new Produkt("Electronics", "Laptop", 1199));
productList.AddProduct(new Produkt("Clothing", "Jeans", 59));
productList.AddProduct(new Produkt("Clothing", "T-shirt", 19));
productList.AddProduct(new Produkt("Furniture", "Table", 159));
productList.AddProduct(new Produkt("Fur", "Chair", 59));

Console.WriteLine("To enter a new product - enter:'N'   To show list - enter:'P'   To search - enter:'S'   To quit - enter: 'Q'");

while (true)
{
    string checkInput = Console.ReadLine().Trim();

    // Check if the user wants to quit the program
    if (checkInput.ToUpper() == "Q")
    {
        break;
    }
    switch(checkInput.ToUpper())
    {
        // Check if the user wants to print the list of products
        case "P":
            Console.WriteLine("Current Products: ");
            productList.PrintProducts();
            break;
        // Check if the user wants to add a new product
        case "N":
            {
                Console.Write("Enter a category: ");
                string category = Console.ReadLine();
                Console.Write("Enter a name: ");
                string name = Console.ReadLine();
                Console.Write("Enter a price: ");
                int price = Convert.ToInt32(Console.ReadLine());

                Produkt newProduct = new Produkt(category, name, price);
                if (!productList.Products.Contains(newProduct))
                {
                    productList.AddProduct(newProduct);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Product added successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product already exists in the list.");
                    Console.ResetColor();
                }
                break;
            }
        // Check if the user wants to search for a product
        case "S":
            {
                Console.Write("Enter a search term: ");
                string searchTerm = Console.ReadLine().ToLower();
                productList.PrintProducts(searchTerm);
                break;
            }
        // If the input is invalid, print an error message
        default:
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
                Console.WriteLine("To enter a new product - enter:'N'   To show list - enter:'P'   To search - enter:'S'   To quit - enter: 'Q'");
                break;
            }
    }
}
// ProduktList class is used to create a list of products and add products to the list
class ProduktList
{
    public ProduktList()
    {
        Products = new List<Produkt>();
    }
    public List<Produkt> Products { get; set; }
    // AddProduct method is used to add a product to the list and sort the list by price
    public void AddProduct(Produkt product)
    {
        Products.Add(product);
        Products = Products.OrderBy(p => p.Price).ToList();
    }
    // PrintProductTitle method is used to print the product title with the search term highlighted
    private void PrintProductTitle(string title, string value, string search = "")
    {
        Console.Write(title + ": ");
        int startIndex = Math.Max(value.ToLower().IndexOf(search.ToLower()),0);
        bool found = value.ToLower().Contains(search.ToLower());
        Console.Write(value.Substring(0, startIndex));
        if (found)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(value.Substring(startIndex, search.Length));
            Console.ResetColor();
        }
        Console.ResetColor();
        int foundLenght = found ? search.Length : 0;
        Console.Write(value.Substring(startIndex + foundLenght, value.Length - startIndex - foundLenght));
    }
    private void PrintProduct(Produkt product, string search = "")
    {
        PrintProductTitle("Category", product.Category.PadRight(20), search);
        PrintProductTitle("Name", product.Name.PadRight(20), search);
        PrintProductTitle("Price", product.Price.ToString(), search);
        Console.Write("\n");
    }
    public void PrintProducts(string search = "")
    {
        foreach (var product in Products)
        {
            PrintProduct(product, search);
        }
        int totalAmount = 0;
        foreach (var pro in Products)
        {
            totalAmount += pro.Price;
        }
        Console.WriteLine($"Total: {totalAmount}".ToString().PadLeft(67));
    }

}
// Produkt class is used to create a product object with category, name, and price
class Produkt
{
    public Produkt(string category, string name, int price)
    {
        Category = category;
        Name = name;
        Price = price;
    }

    public string Category { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    // Equals method is used to compare two product objects
    public override bool Equals(object? x)
    {
        var product = x as Produkt;
        if (product == null)
        {
            return false;
        }
        return this.Category == product.Category && this.Name == product.Name && this.Price == product.Price;
    }
}