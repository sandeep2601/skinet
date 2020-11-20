using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using Core.Entities;
using Core.Specifications;

namespace API.Contract
{
    public interface IProductManager
    {
        Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams);
        Task<ProductToReturnDto> GetProduct(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrands();
        Task<IReadOnlyList<ProductType>> GetProductTypes();
        Task<bool> SaveProductDetails(Product productDetails);
    }
}