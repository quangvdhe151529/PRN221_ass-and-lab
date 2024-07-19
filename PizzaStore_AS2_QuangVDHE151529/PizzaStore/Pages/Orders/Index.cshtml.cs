using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get;set; } = default!;

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return RedirectToPage("/Login");
            }
            if (_context.Orders != null)
            {
                Orders = await _context.Orders.Include(o => o.Customer).ToListAsync();
            }
            ViewData["Products"] = await _context.Products.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string action = Request.Form["Action"];
            if (action == "CREATE")
            {
                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    CustomerId = Order.CustomerId,
                    OrderDate = DateTime.Now,
                    RequiredDate = Order.RequiredDate,
                    ShippedDate = null,
                    Freight = Order.Freight,
                    ShipAddress = Order.ShipAddress
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
}
