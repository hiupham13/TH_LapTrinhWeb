namespace Week2_PhamHoangPhiHieu.Models
{
    public class ProductIndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
