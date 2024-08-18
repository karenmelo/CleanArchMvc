using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetProductsAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product == null) return NotFound();

            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.Update(product);
                }
                catch (Exception ex)
                {

                    throw new ApplicationException(ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product == null) return NotFound();

            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + product.Image);
            var exists = System.IO.File.Exists(image);

            ViewBag.ImageExist = exists;

            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
