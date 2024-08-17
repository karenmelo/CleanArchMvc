using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Application.Services.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IMediator mediator)
    {

        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Add(ProductDto productDTO)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);

        if (productCreateCommand == null)
            throw new ApplicationException("Entity could not be loaded");

        await _mediator.Send(productCreateCommand);
    }

    public async Task Delete(int id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id);

        if (productRemoveCommand == null)
            throw new ApplicationException("Entity could not be loaded");

        await _mediator.Send(productRemoveCommand);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var productQuery = new GetProductByIdQuery(id);


        if (productQuery == null)
            throw new ApplicationException("Entity could not be loaded");

        var result = await _mediator.Send(productQuery);
        return _mapper.Map<ProductDto>(result);
    }

    public async Task<ProductDto> GetProductCategoryAsync(int id)
    {
        var productCategoryQuery = new GetProductByIdQuery(id);

        if (productCategoryQuery == null)
            throw new ApplicationException("Entity could not be loaded");

        var result = await _mediator.Send(productCategoryQuery);
        return _mapper.Map<ProductDto>(result);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery == null)
            throw new ApplicationException("Entity could not be loaded");

        var result = await _mediator.Send(productsQuery);
        return _mapper.Map<IEnumerable<ProductDto>>(result);
    }

    public async Task Update(ProductDto productDTO)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);

        if (productUpdateCommand == null)
            throw new ApplicationException("Entity could not be loaded");

        await _mediator.Send(productUpdateCommand);
    }
}
