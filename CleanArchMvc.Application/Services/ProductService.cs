using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Services.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper;
    }

    public async Task Add(ProductDto productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.CreateAsync(productEntity);
    }

    public async Task Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        await _productRepository.RemoveAsync(product);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(productEntity);
    }

    public async Task<ProductDto> GetProductCategoryAsync(int id)
    {
        var productCategoryEntity = await _productRepository.GetProductCategoryAsync(id);
        return _mapper.Map<ProductDto>(productCategoryEntity);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var productsEntity = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(productsEntity);
    }

    public async Task Update(ProductDto productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        await _productRepository.UpdateAsync(product);
    }
}
