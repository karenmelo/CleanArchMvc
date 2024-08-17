using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<ProductDTO> GetProductCategoryAsync(int id);
    Task Add(ProductDTO productDTO);
    Task Update(ProductDTO productDTO);
    Task Delete(int id);

}
