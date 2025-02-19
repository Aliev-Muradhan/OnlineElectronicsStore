using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineElectronicsStore.Models;
using OnlineElectronicsStore.Services;

namespace OnlineElectronicsStore.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
		private readonly IWebHostEnvironment _environment;
		private readonly ApplicationDbContext _context;

		[BindProperty]
		public ProductDto ProductDto { get; set; } = new ProductDto();

		public Product Product { get; set; } = new Product();

		public string errorMessage = "";
		public string successMessage = "";

		public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
		{
			_environment = environment;
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

			ProductDto.Name = product.Name;
			ProductDto.Brand = product.Brand;
			ProductDto.Category = product.Category;
			ProductDto.Price = product.Price;
			ProductDto.Description = product.Description;

			Product = product;
		}

		public void OnPost(int? id)
		{
			if (id == null)
			{
				Response.Redirect("/Admin/Products/Index");
				return;
			}
			if (!ModelState.IsValid)
			{
				errorMessage = "Please provide all the required fields";
				return;
			}

			var product = _context.Products.Find(id);
			if (product == null)
			{
				Response.Redirect("/Admin/Products/Index");
				return;
			}

			string newFileName = product.ImageFileName;
			if (ProductDto.ImageFile != null)
			{
				newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(ProductDto.ImageFile.FileName);

				string imageFullPath = _environment.WebRootPath + "/products/" + newFileName;
				using (var stream = System.IO.File.Create(imageFullPath))
				{
					ProductDto.ImageFile.CopyTo(stream);
				}

				string oldImageFullPath = _environment.WebRootPath + "/products/" + product.ImageFileName;
				System.IO.File.Delete(oldImageFullPath);
			}

			product.Name = ProductDto.Name;
			product.Brand = ProductDto.Brand;
			product.Category = ProductDto.Category;
			product.Price = ProductDto.Price;
			product.Description = ProductDto.Description ?? "";
			product.ImageFileName = newFileName;

			_context.SaveChanges();

			Product = product;

			successMessage = "Product updated successfully";

			Response.Redirect("/Admin/Products/Index");
		}
	}
}
