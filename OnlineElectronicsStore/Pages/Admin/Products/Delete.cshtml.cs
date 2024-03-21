using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineElectronicsStore.Services;

namespace OnlineElectronicsStore.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
		private readonly IWebHostEnvironment _environment;
		private readonly ApplicationDbContext _context;

		public DeleteModel(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
		{
			_environment = webHostEnvironment;
			_context = context;
		}
		public void OnGet(int? id)
		{
			if (id == null)
			{
				Response.Redirect("/Admin/Products/Index");
				return;
			}

			var product = _context.Products.Find(id);
			if (product == null)
			{
				Response.Redirect("/Admin/Products/Index");
				return;
			}
			string imageFullPath = _environment.WebRootPath + "/products/" + product.ImageFileName;
			System.IO.File.Delete(imageFullPath);

			_context.Products.Remove(product);
			_context.SaveChanges();

			Response.Redirect("/Admin/Products/Index");
		}
	}
}
