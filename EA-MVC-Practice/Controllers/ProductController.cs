using EA_MVC_Practice.Database;
using EA_MVC_Practice.DTO;
using EA_MVC_Practice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EA_MVC_Practice.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext _context;

        public ProductController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Count"] = 0;
            List<Product> products = await _context.Products.ToListAsync();
            return View(viewName: "Index", model: products);
        }


        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"{id}");
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var dtoProduct = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            };
            return View(viewName: "Edit", model: dtoProduct);
        }

        [HttpPost]
        [Route("Product/Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductDTO product)
        {

            var editProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (editProduct == null)
                return NotFound();
            editProduct.Name = product.Name;
            editProduct.Description = product.Description;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Add(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", product);
            }

            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
