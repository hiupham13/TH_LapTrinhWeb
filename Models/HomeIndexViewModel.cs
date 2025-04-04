using System.Collections.Generic;

namespace Week2_PhamHoangPhiHieu.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}