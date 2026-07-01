using YFCore.Domain.ProductEntity;
using YFCore.Domain.Shared.Repository;

namespace YFCore.Domain.ProductRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetAllByCategoryIdAsync(Guid categoryId);
        Task<ICollection<Product>> GetAllWithCategoryAsync();
        Task<Product?> GetByIdWithCategoryAsync(Guid id);
    }
}
