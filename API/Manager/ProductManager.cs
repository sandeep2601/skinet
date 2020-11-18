using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Manager
{
    public class ProductManager
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductManager(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }
        
        public async Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var toolItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);
            
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, toolItems, data);
        }

        public async Task<Product> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            return product;
        }
        
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await _productBrandRepo.ListAllAsync();
        }
        
        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            return await _productTypeRepo.ListAllAsync();
        }
    }
}