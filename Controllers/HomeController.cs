using Microsoft.AspNetCore.Mvc;
using Week2_PhamHoangPhiHieu.Models;
using Week2_PhamHoangPhiHieu.Repositories;

namespace Week2_PhamHoangPhiHieu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                FeaturedProducts = await _productRepository.GetAllAsync(), // Lấy 4 sản phẩm đầu tiên (có thể tùy chỉnh logic để lấy sản phẩm nổi bật)
                Categories = await _categoryRepository.GetAllAsync()
            };
            return View(model);
        }
    }
}