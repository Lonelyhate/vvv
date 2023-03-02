using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.BrandResponseModels;
using Services.ProductAPI.Models.RequsetModels.BrandRequestModels;
using Services.ProductAPI.Models.ViewModels;
using Services.ProductAPI.Repository.Interfaces;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }
    
    public async Task<BrandCreateResponseModel> CreateBrand(BrandCreateRequestModel model)
    {
        try
        {
            var response = new BrandCreateResponseModel();

            var brand = await _brandRepository.GetByName(model.Name);
            if (brand is not null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Бренд с таким названием уже есть";
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }
            brand = await _brandRepository.Create(new Brand {Name = model.Name});
            response.Data = new BrandViewModel { Id = brand.Id, Name = brand.Name };
            response.StatusCode = StatusCode.Created;
            return response;
        }
        catch(Exception e)
        {
            return new BrandCreateResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()},
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BrandGetAllResponseModel> GetBrands()
    {
        try
        {
            var response = new BrandGetAllResponseModel();
            var brands = await _brandRepository.GetAll();
            
            var brandsResponse = new List<BrandViewModel>();
            foreach (var brand in brands)
            {
                brandsResponse.Add(new BrandViewModel{Id = brand.Id, Name = brand.Name});
            }

            response.Data = brandsResponse.OrderBy(b => b.Name).ToList();
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch(Exception e)
        {
            return new BrandGetAllResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()},
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}