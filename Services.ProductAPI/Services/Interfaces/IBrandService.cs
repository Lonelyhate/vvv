using Services.ProductAPI.Models.BrandResponseModels;
using Services.ProductAPI.Models.RequsetModels.BrandRequestModels;

namespace Services.ProductAPI.Services.Interfaces;

public interface IBrandService
{
    Task<BrandCreateResponseModel> CreateBrand(BrandCreateRequestModel model);
    
    Task<BrandGetAllResponseModel> GetBrands();
}