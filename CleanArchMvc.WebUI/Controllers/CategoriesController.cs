using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

[Authorize]
public class CategoriesController : Controller
{   
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;        
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _categoryService.GetCategoriesAsync();
        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.Add(category);
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null) return NotFound();


        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _categoryService.Update(category);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    [HttpGet()]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var category = await _categoryService.GetCategoryByIdAsync(id.Value);

        if (category == null) return NotFound();

        return View(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null) return NotFound();

        return View(category);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        await _categoryService.Delete(id.Value);
        return RedirectToAction(nameof(Index));
    }

}
