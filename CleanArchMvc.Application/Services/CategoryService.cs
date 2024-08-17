using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper;
    }

    public async Task Add(CategoryDto categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.Create(categoryEntity);
    }

    public async Task Delete(int id)
    {
        var category = await _categoryRepository.GetById(id);
        await _categoryRepository.Remove(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categoriesEntity = await _categoryRepository.GetCategories();
        return _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDto>(categoryEntity);
    }

    public async Task Update(CategoryDto categoryDTO)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.Update(categoryEntity);
    }
}
