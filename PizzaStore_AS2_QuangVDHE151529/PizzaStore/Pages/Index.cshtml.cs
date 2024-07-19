using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PizzaStore.Data.AppDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Product> Products { get; set; } = default!;
        public string CurrentFilter { get; set; }
        public string CategoryId { get; set; }
        public async Task<IActionResult> OnGetAsync(string searchString, string categoryId)
        {
            CurrentFilter = searchString;

            IQueryable<Product> productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier);

            if (!String.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int id))
                {
                    productsQuery = productsQuery.Where(p => p.ProductId == id);
                }
                else
                {
                    productsQuery = productsQuery.Where(p =>
                        p.ProductName.Contains(searchString) ||
                        p.UnitPrice.ToString().Contains(searchString));
                }
            }
            if (!String.IsNullOrEmpty(categoryId))
            {
                CategoryId = categoryId;
                int categoryIdInt;
                if (int.TryParse(categoryId, out categoryIdInt))
                {
                    productsQuery = productsQuery.Where(p => p.CategoryId == categoryIdInt);
                }
            }

            Products = await productsQuery.ToListAsync();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName"); //trả về danh sách id
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName"); //trả về danh sách id

            return Page();
        }
    }
}
