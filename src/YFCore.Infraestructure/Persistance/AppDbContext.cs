using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YFCore.Domain.Categories.Entity;
using YFCore.Domain.ProcedureTypes.Entity;
using YFCore.Infraestructure.Models.Categories;

namespace YFCore.Infraestructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
