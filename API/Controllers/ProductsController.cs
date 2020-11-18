using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Specifications;
using API.DTOs;
using API.Errors;
using API.Helpers;
using API.Contract;
using AutoMapper;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public ProductsController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            Pagination<ProductToReturnDto> data = await _productManager.GetProducts(productParams);
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            Product product = await _productManager.GetProduct(id);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            IReadOnlyList<ProductBrand> productBrands = await _productManager.GetProductBrands();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            IReadOnlyList<ProductType> productTypes = await _productManager.GetProductTypes();
            return Ok(productTypes);
        }
    }
}