using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PizzaStore.Models;
using System.Globalization;

namespace PizzaStore.Pages.Cart
{
    public class RemoveCartModel : PageModel
    {
        public List<CartItem> Carts { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int productId)
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                Carts = JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
                var cartitem = Carts.Find(p => p.ProductId == productId);
                if (cartitem != null)
                {
                    Carts.Remove(cartitem);
                }
                string jsoncart1 = JsonConvert.SerializeObject(Carts);
                session.SetString("Cart", jsoncart1);
            }
            return RedirectToPage("/Cart/Index");
        }
    }
}
