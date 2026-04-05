using ECommerceApp.Models;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly AppDbContext _context;

        public OrderController(IRepository<Order> orderRepo, AppDbContext context)
        {
            _orderRepo = orderRepo;
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToList();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _orderRepo.GetById(id);
            if (order == null) return NotFound();
            _orderRepo.Delete(id);
            _orderRepo.Save();
            return RedirectToAction("Index");
        }
    }
}