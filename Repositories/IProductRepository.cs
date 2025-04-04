using Week2_PhamHoangPhiHieu.Models;

namespace Week2_PhamHoangPhiHieu.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize);
    }
}