using ECommerceApp.Models;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ECommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _productRepo;

        public CartController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (cartJson == null) return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == id);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null) cart.Remove(item);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
    }
}