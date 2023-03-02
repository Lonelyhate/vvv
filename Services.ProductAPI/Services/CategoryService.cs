using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.CategoryResponseModels;
using Services.ProductAPI.Models.RequsetModels.CategoryRequestModels;
using Services.ProductAPI.Models.ViewModels;
using Services.ProductAPI.Repository.Interfaces;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<CategoryCreateResponseModel> CategoryCreate(CategoryCreateRequestModel model)
    {
        try
        {
            var response = new CategoryCreateResponseModel();

            var category = await _categoryRepository.GetByName(model.Name);
            if (category is not null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Категория с таким названием уже есть";
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            var categoryResponse = await _categoryRepository.Create(new Category { Name = model.Name });
            response.Data = new CategoryViewModel { Id = categoryResponse.Id, Name = categoryResponse.Name };
            response.StatusCode = StatusCode.Created;
            return response;
        }
        catch (Exception e)
        {
            return new CategoryCreateResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()},
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<CategoryGetAllResponseModel> CategoryGetAll()
    {
        try
        {
            var response = new CategoryGetAllResponseModel();
            var categories = await _categoryRepository.GetAll();
            var categoriesResponse = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                categoriesResponse.Add(new CategoryViewModel {Id = category.Id, Name = category.Name});
            }

            response.Data = categoriesResponse.OrderBy(c => c.Name).ToList();
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new CategoryGetAllResponseModel
            {
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()},
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}