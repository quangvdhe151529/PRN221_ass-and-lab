using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStore.Data.AppDbContext _context;

        public IndexModel(PizzaStore.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get;set; } = default!;

        [BindProperty]
        public Product Product { get; set; }

        //GET
        public async Task<IActionResult> OnGetAsync()
        {
            //Author
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            //if (type != 1)
            //{
            //    return RedirectToPage("/Login");
            //}

            if (_context.Products != null)
            {
                Products = await _context.Products
               .Include(p => p.Category)
               .Include(p => p.Supplier).ToListAsync();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName"); //trả về danh sách id
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName"); //trả về danh sách id
            return Page();
        }

        //POST
        public async Task<IActionResult> OnPostAsync()
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return RedirectToPage("/Login");
            }

            string actionValue = Request.Form["Action"];
            if (actionValue == "CREATE")
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
            }
            else if(actionValue == "EDIT")
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == Product.ProductId);
                if(product != null)
                {
                    product.ProductName = Product.ProductName;
                    product.SupplierId = Product.SupplierId;
                    product.CategoryId = Product.CategoryId;
                    product.QuantityPerUnit = Product.QuantityPerUnit;
                    product.UnitPrice = Product.UnitPrice;
                    product.ProductImage = Product.ProductImage;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
