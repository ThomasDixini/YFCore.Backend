using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Categories.Repository;
using YFCore.Infraestructure.Models.Categories;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Shared;

namespace YFCore.Infraestructure.Repository.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<Category, TResult>> selector)
        {
            return await _context.Categories.Select(selector).ToListAsync();
        }
    }
}
