using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YFCore.Domain.ProductEntity;
using YFCore.Domain.ProductRepository;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Shared;

namespace YFCore.Infraestructure.Repository.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
        public async Task<ICollection<Product>> GetAllByCategoryIdAsync(Guid categoryId)
        {
            return await _context.Products.Where(c => c.CategoryId == categoryId).ToListAsync();
        }
    }
}