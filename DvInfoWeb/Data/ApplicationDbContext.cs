using DvInfoWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DvInfoWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; } //DbSEt creates table. <> = Which table, prop name is Table name to be created in sql server......

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "History", DisplayOrder = 3 }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
