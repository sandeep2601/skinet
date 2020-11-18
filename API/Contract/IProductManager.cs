using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Contract
{
    public interface IProductManager
    {
        Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams);
        Task<Product> GetProduct(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrands();
        Task<IReadOnlyList<ProductType>> GetProductTypes();
    }
}