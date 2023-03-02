using Services.ProductAPI.Models;
using Services.ProductAPI.Models.RequsetModels;
using Services.ProductAPI.Models.ViewModels;

namespace Services.ProductAPI.Services.Interfaces;

public interface IProductService
{
    Task<ProductCreateResponseModel> CreateProduct(ProductFormViewModel requestModel);

    Task<ProductGetAllResponseModel> GetAllProducts(string? category, string? orderBy, int? take);

    Task<ProductGetResponseModel> GetProductById(int id);

    Task<ProductDeleteResponseModel> DeleteProduct(int id);

    Task<ProductUpdateResponseModel> UpdateProduct(ProductUpdateRequestModel requestModel);
}