using System.Collections.Generic;
using System.Threading.Tasks;
using API.Contract;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace API.Manager
{
    public class ProductManager : IProductManager
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

        public async Task<ProductToReturnDto> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            var productData = _mapper.Map<Product, ProductToReturnDto>(product);
            return productData;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await _productBrandRepo.ListAllAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            return await _productTypeRepo.ListAllAsync();
        }

        public async Task<bool> SaveProductDetails(Product productDetails)
        {
            // ProductType productTypeName = new ProductType();
            // productTypeName.Name = productDetails.ProductType;
            
            // ProductBrand productBrandName = new ProductBrand();
            // productBrandName.Name = productDetails.ProductBrand;
            
            // Product product = new Product()
            // {
            //     Id = productDetails.Id,
            //     Name = productDetails.Name,
            //     Description = productDetails.Description,
            //     Price = productDetails.Price,
            //     PictureUrl = productDetails.PictureUrl,
            //     // ProductType = productTypeName,
            //     // ProductBrand = productBrandName,
            //     ProductTypeId = productDetails.ProductTypeId,
            //     ProductBrandId = productDetails.ProductBrandId,
            // };
            var entries = await _productsRepo.SaveAsync(productDetails);
            System.Console.Write(entries);
            return entries > 0 ? true : false;
        }
    }
}