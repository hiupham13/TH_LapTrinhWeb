using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Week2_PhamHoangPhiHieu.Models;
using Week2_PhamHoangPhiHieu.Repositories;

namespace Week2_PhamHoangPhiHieu.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public CartController(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        // GET: Cart
        [Authorize] // Yêu cầu người dùng đăng nhập
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToList();
            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [Authorize] // Yêu cầu người dùng đăng nhập
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            // Kiểm tra sản phẩm có tồn tại không
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                TempData["Error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction("Index", "Product");
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                _context.Carts.Update(cartItem);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã thêm sản phẩm vào giỏ hàng!";
            return RedirectToAction("Index");
        }
    }
}