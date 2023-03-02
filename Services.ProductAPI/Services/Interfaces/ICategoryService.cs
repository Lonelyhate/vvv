using Services.ProductAPI.Models.CategoryResponseModels;
using Services.ProductAPI.Models.RequsetModels.CategoryRequestModels;

namespace Services.ProductAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryCreateResponseModel> CategoryCreate(CategoryCreateRequestModel model);

    Task<CategoryGetAllResponseModel> CategoryGetAll();
}