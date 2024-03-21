using Microsoft.EntityFrameworkCore;
using OnlineElectronicsStore.Models;

namespace OnlineElectronicsStore.Services
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
