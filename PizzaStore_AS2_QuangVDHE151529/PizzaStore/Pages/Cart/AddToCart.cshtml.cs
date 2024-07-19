using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PizzaStore.Data;
using PizzaStore.Models;
using System.Globalization;

namespace PizzaStore.Pages.Cart
{
    public class AddToCartModel : PageModel
    {
        private readonly AppDbContext _context;
        public AddToCartModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound("Không có sản phẩm đc thêm");
            }
            //Thêm vào cart
            var cart = GetCartItems();
            var cartitem = cart.Find(c => c.ProductId == id);
            if (cartitem != null)
            {
                cartitem.Quantity++;
                cartitem.UnitPrice += product.UnitPrice;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() {ProductId = product.ProductId, 
                    ProductName = product.ProductName, 
                    Quantity = 1,
                    UnitPrice = product.UnitPrice,
                    ProductImage= product.ProductImage});
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToPage("/Index");
        }

        // Lấy cart từ Session (danh sách CartItem)
        private List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("Cart");
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("Cart", jsoncart);
        }

    }
}
