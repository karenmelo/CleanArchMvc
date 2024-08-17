using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);

    Task Add(CategoryDto categoryDTO);
    Task Update(CategoryDto categoryDTO);
    Task Delete(int id);
}
