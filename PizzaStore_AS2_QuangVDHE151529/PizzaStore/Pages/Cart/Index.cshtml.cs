using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PizzaStore.Models;

namespace PizzaStore.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStore.Data.AppDbContext _context;

        public IndexModel(PizzaStore.Data.AppDbContext context)
        {
            _context = context;
        }
        public List<CartItem> Carts { get; set; } = default!;
        public double TotalPrice { get; set; }
        [BindProperty]
        public Order Order { get; set; }

        //GET
        public async Task<IActionResult> OnGetAsync()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                Carts = JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
                TotalPrice = Carts.Sum(item => item.UnitPrice * item.Quantity);
            }
            return Page();
        }

        //POST
        public async Task<IActionResult> OnPostAsync()
        {
            //int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            //if (type != 1)
            //{
            //    return RedirectToPage("/Login");
            //}

            //Xử lý lấy cart
            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                Carts = JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
                TotalPrice = Carts.Sum(item => item.UnitPrice * item.Quantity);
            }

            //Xử lý thêm Order
            var customer = await _context.Customers.FirstOrDefaultAsync();
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customer.CustomerId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                ShippedDate = null,
                Freight = Order.Freight,
                ShipAddress = customer.Address
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");

            return RedirectToPage("/Orders/Index");
        }
    }
}
