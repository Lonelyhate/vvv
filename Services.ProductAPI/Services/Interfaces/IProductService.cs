using Services.ProductAPI.Models;
using Services.ProductAPI.Models.RequsetModels;
using Services.ProductAPI.Models.ViewModels;

namespace Services.ProductAPI.Services.Interfaces;

public interface IProductService
{
    Task<ProductCreateResponseModel> CreateProduct(ProductFormViewModel requestModel);

    Task<ProductGetAllResponseModel> GetAllProducts(string? search, string? category, string? orderBy, int? take, int? priceMin, int? priceMax, string? gender, string? sizes, string? brands, Sort? sort, bool? newProducts = false);

    Task<ProductGetResponseModel> GetProductById(int id);

    Task<ProductDeleteResponseModel> DeleteProduct(int id);

    Task<ProductUpdateResponseModel> UpdateProduct(ProductUpdateRequestModel requestModel);
}