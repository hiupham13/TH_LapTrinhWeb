using Week2_PhamHoangPhiHieu.Models;
using Microsoft.EntityFrameworkCore;

namespace Week2_PhamHoangPhiHieu.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category) // Include thông tin về category 
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category) // Lấy thông tin kèm theo category 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // Triển khai phương thức GetTotalCountAsync
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        // Triển khai phương thức GetPagedAsync
        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Products
                .Include(p => p.Category) // Giữ nguyên Include để lấy thông tin Category
                .Skip((page - 1) * pageSize) // Bỏ qua các sản phẩm của trang trước
                .Take(pageSize) // Lấy số lượng sản phẩm tối đa cho trang hiện tại
                .ToListAsync();
        }
    }
}