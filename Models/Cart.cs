using System.ComponentModel.DataAnnotations.Schema;
using Week2_PhamHoangPhiHieu.Models;
namespace Week2_PhamHoangPhiHieu.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}