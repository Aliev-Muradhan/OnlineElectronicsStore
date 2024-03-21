using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineElectronicsStore.Models;
using OnlineElectronicsStore.Services;

namespace OnlineElectronicsStore.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
		private readonly ApplicationDbContext _context;

		public List<Product> Products { get; set; } = new List<Product>();
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
			Products = _context.Products.OrderByDescending(p => p.Id).ToList();
		}
	}
}
