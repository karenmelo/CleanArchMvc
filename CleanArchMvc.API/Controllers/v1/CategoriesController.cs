using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        if (categories == null) return NotFound("Categories not found");

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null) return NotFound("Category not found");


        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CategoryDto category)
    {
        if (category == null) return BadRequest("Invalid data");

        await _categoryService.Add(category);

        return new CreatedAtRouteResult("GetCategory", new { id = category.Id }, category);
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, [FromBody] CategoryDto category)
    {
        if (id != category.Id) return BadRequest("Invalid data");

        if (category == null) return BadRequest("Invalid data");

        await _categoryService.Update(category);


        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null) return NotFound("Category not found");

        await _categoryService.Delete(id);

        return Ok(category);
    }
}
