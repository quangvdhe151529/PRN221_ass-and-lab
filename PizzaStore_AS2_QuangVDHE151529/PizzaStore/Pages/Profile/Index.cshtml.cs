using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStore.Data.AppDbContext _context;

        public IndexModel(PizzaStore.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        [BindProperty]
        public Customer Customer { get; set; }


        //GET
        public async Task OnGetAsync()
        {
            Customer =await _context.Customers.FirstOrDefaultAsync();
            if (_context.Orders != null)
            {
                Order = await _context.Orders.Include(o => o.Customer).ToListAsync();
            }
        }

        //POST
        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }
    }
}
