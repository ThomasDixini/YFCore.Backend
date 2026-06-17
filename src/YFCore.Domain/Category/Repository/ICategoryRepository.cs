using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using YFCore.Domain.Categories.Entity;
using YFCore.Domain.Shared.Repository;

namespace YFCore.Domain.Categories.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<Category, TResult>> selector);
    }
}
