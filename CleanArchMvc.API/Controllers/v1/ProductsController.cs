using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetProductsAsync();

        if(products == null) return NotFound("Products not found");

        return Ok(products);
    }


    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null) return NotFound("Product not found");

        return Ok(product);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto product)
    {

        if (product == null) return BadRequest("Invalid data");

        await _productService.Add(product);

        return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductDto product)
    {
        if (id != product.Id) return BadRequest("Invalid data");

        if (product == null) return BadRequest("Invalid data");

        await _productService.Update(product);

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null) return NotFound("Product not found");

        await _productService.Delete(id);

        return Ok(product);

    }
}
