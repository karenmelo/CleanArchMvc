using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task Add(ProductDto productDTO);
    Task Update(ProductDto productDTO);
    Task Delete(int id);

}
