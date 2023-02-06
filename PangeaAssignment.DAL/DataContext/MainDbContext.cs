using Microsoft.EntityFrameworkCore;
using PangeaAssignment.Models;

namespace PangeaAssignment.DAL.DataContext
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        public DbSet<InputModel> Comparisons { get; set; }

        protected override void onModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}
