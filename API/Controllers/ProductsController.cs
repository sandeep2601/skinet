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
using System;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
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
            ProductToReturnDto productData = await _productManager.GetProduct(id);
            if (productData == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return productData;
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
        
        [HttpPost("save")]
        public async Task<IActionResult> SaveProductDetails(Product productDetails)
        {
            Console.Write(productDetails);
            var response = await _productManager.SaveProductDetails(productDetails);
            Console.Write(response);
            return Ok(response);
        }
    }
}