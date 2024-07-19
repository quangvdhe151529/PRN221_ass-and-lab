using PizzaStore.Models;
using PizzaStore.Models.Enum;

namespace PizzaStore.Data
{
    public static class DbInitializer
    {
        //Khoi toa du lieu neu như khong co
        public static void Initialize(AppDbContext context)
        {

            //Category
            if (!context.Categories.Any())
            {
                List<Category> categories = new List<Category>
                {
                    new Category { CategoryName = "Meat", Description = "For meat eaters" },
                    new Category { CategoryName = "Vegan", Description = "For meat haters" },
                    new Category { CategoryName = "Seafood", Description = "For seafood lovers" },
                    new Category { CategoryName = "Desserts", Description = "For those with a sweet tooth" },
                    new Category { CategoryName = "Drinks", Description = "For refreshment" },
                    new Category { CategoryName = "Vegetables", Description = "For health-conscious eaters" },
                    new Category { CategoryName = "Fruits", Description = "For natural sweetness" },
                    new Category { CategoryName = "Snacks", Description = "For quick bites" },
                    new Category { CategoryName = "Pasta", Description = "For Italian cuisine lovers" },
                    new Category { CategoryName = "Salads", Description = "For light and healthy meals" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            };

            //Suppliers
            if (!context.Suppliers.Any())
            {
                List<Supplier> suppliers = new List<Supplier>
                {
                    new Supplier { CompanyName = "Pizza Hut", Address = "USA", Phone = "0123456789" },
                    new Supplier { CompanyName = "Al Fresco", Address = "Germany", Phone = "0123456788" },
                    new Supplier { CompanyName = "Domino's", Address = "Vietnam", Phone = "0888666777" },
                    new Supplier { CompanyName = "Italiano Pizza", Address = "Italy", Phone = "0123456787" },
                    new Supplier { CompanyName = "PizzaExpress", Address = "UK", Phone = "0123456786" },
                    new Supplier { CompanyName = "Papa John's", Address = "USA", Phone = "0123456785" },
                    new Supplier { CompanyName = "Sbarro", Address = "USA", Phone = "0123456784" },
                    new Supplier { CompanyName = "Little Caesars", Address = "USA", Phone = "0123456783" },
                    new Supplier { CompanyName = "California Pizza Kitchen", Address = "USA", Phone = "0123456782" },
                    new Supplier { CompanyName = "Pizza Inn", Address = "USA", Phone = "0123456781" }
                };

                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }

            //Product
            if (!context.Products.Any())
            {
                List<Product> products = new List<Product>
                {
                    new Product {ProductName="Peperoni", SupplierId = 1, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 10.99, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150"},
                    new Product {ProductName="Vegan", SupplierId = 1, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 7.5, ProductImage= "https://biancazapatka.com/wp-content/uploads/2020/05/quinoa-pizza-mit-spargel.jpg" },
                    new Product {ProductName="Cheese Pizza", SupplierId = 2, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 8.99, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="Margherita", SupplierId = 2, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 9.99, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="Hawaiian", SupplierId = 3, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 11.99, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="Mushroom", SupplierId = 3, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 9.5, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="BBQ Chicken", SupplierId = 4, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 12.99, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="Spinach", SupplierId = 4, CategoryId = 1, QuantityPerUnit = 100, UnitPrice = 9.75, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="Vegetarian", SupplierId = 5, CategoryId = 2, QuantityPerUnit = 100, UnitPrice = 8.5, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" },
                    new Product {ProductName="Margarita", SupplierId = 5, CategoryId = 2, QuantityPerUnit = 100, UnitPrice = 7.99, ProductImage= "https://foodhub.scene7.com/is/image/woolworthsltdprod/2004-easy-pepperoni-pizza:Mobile-1300x1150" }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            //Account
            if (!context.Accounts.Any())
            {
                List<Account> accounts = new List<Account>
                {
                    new Account { FullName = "Ngoc Manh", Password = "123456", Username = "ngocmanh", Type = AccountType.Staff},
                    new Account { FullName = "Hai Lan", Password = "123456", Username = "hailan", Type = AccountType.Normal}
                };

                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }

            //Customer
            Customer customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                ContactName = "Test",
                Address = "Hanoi",
                Password = "Password",
                Phone = "1234567890"
            };

            if (!context.Customers.Any())
            {
                List<Customer> customers = new List<Customer>
                {
                    customer,
                };
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            //Order
            Order order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customer.CustomerId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                ShippedDate = null,
                Freight = 100.0,
                ShipAddress = "Ha Noi"
            };

            if (!context.Orders.Any())
            {
                List<Order> orders = new List<Order>
                {
                    order
                };
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
