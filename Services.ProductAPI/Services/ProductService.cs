using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.RequsetModels;
using Services.ProductAPI.Models.ViewModels;
using Services.ProductAPI.Repository.Interfaces;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductCreateResponseModel> CreateProduct(ProductFormViewModel requestModel)
    {
        try
        {
            ProductCreateResponseModel responseModel = new ProductCreateResponseModel();
            var productCheck = await _productRepository.GetByName(requestModel.Name);
            if (productCheck is not null)
            {
                responseModel.isSuccess = false;
                responseModel.DisplayMessage = "Товар с такими именем уже есть";
                responseModel.StatusCode = StatusCode.BadRequest;
                return responseModel;
            }

            var category = await _categoryRepository.GetById(requestModel.CategoryId);
            if (category is null)
            {
                responseModel.isSuccess = false;
                responseModel.DisplayMessage = "Такой категории не существует";
                responseModel.StatusCode = StatusCode.BadRequest;
                return responseModel;
            }
            
            var brand = await _brandRepository.GetById(requestModel.BrandId);
            if (brand is null)
            {
                responseModel.isSuccess = false;
                responseModel.DisplayMessage = "Такого бренда не существует";
                responseModel.StatusCode = StatusCode.BadRequest;
                return responseModel;
            }
            
            var product = new Product
            {
                Name = requestModel.Name,
                BrandId = requestModel.BrandId,
                Brand = brand,
                CategoryId = requestModel.CategoryId,
                Category = category,
                Sizes = requestModel.Sizes,
                Gender = requestModel.Gender,
                Price = requestModel.Price,
                CodeProduct = requestModel.CodeProduct,
                Description = requestModel.Description
            };
            List<string> imgs = new List<string>(); 
            foreach (var imgFile in requestModel.Images)
            {
                string name = $"{Guid.NewGuid()}.{imgFile.ContentType.Split('/')[1]}";
                string path = Path.Combine(_webHostEnvironment.WebRootPath, name);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    imgFile.CopyTo(stream);
                }
                imgs.Add(name);
            }

            product.Images = string.Join(';', imgs);

            var productCreated = await _productRepository.Create(product);
            responseModel.Data = _mapper.Map<ProductViewModel>(productCreated);
            responseModel.StatusCode = StatusCode.Created;
            return responseModel;
        }
        catch (Exception e)
        {
            return new ProductCreateResponseModel
            {
                isSuccess = false,
                StatusCode = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<ProductGetAllResponseModel> GetAllProducts(string? category, string? orderBy, int? take)
    {
        try
        {
            ProductGetAllResponseModel response = new ProductGetAllResponseModel();

            var products = new List<Product>();
            if (category is null)
            {
               products = await _productRepository.GetAll();
            }

            if (category is not null)
            {
                products = await _productRepository.GetByCategory(category);
            }
            if (products.Count == 0)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Товар не найден";
                response.StatusCode = StatusCode.NoContent;
                return response;
            }

            if (orderBy == "desc")
            {
                products = products.OrderByDescending(p => p.DateCreated).ToList();
            }

            if (take is not null)
            {
                products = products.Take((int)take).ToList();
            }

            response.Data = _mapper.Map<List<ProductViewModel>>(products);
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new ProductGetAllResponseModel()
            {
                isSuccess = false,
                StatusCode = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<ProductGetResponseModel> GetProductById(int id)
    {
        try
        {
            var response = new ProductGetResponseModel();

            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Товар не найден";
                response.StatusCode = StatusCode.NotFound;
                return response;
            }

            response.Data = _mapper.Map<ProductViewModel>(product);
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new ProductGetResponseModel()
            {
                isSuccess = false,
                StatusCode = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<ProductDeleteResponseModel> DeleteProduct(int id)
    {
        try
        {
            var response = new ProductDeleteResponseModel();

            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Товар для удаления не найден";
                response.StatusCode = StatusCode.NotFound;
                return response;
            }

            await _productRepository.Delete(product);
            response.Data = true;
            response.DisplayMessage = "Товар удален";
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new ProductDeleteResponseModel()
            {
                isSuccess = false,
                StatusCode = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }

    public async Task<ProductUpdateResponseModel> UpdateProduct(ProductUpdateRequestModel requestModel)
    {
        try
        {
            var response = new ProductUpdateResponseModel();

            var productCheck = await _productRepository.GetById(requestModel.Id);
            if (productCheck is null)
            {
                response.isSuccess = false;
                response.StatusCode = StatusCode.BadRequest;
                response.DisplayMessage = "Данный продукт не найден";
                return response;
            }

            var product = await _productRepository.Update(_mapper.Map<Product>(requestModel));
            response.Data = _mapper.Map<ProductViewModel>(product);
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new ProductUpdateResponseModel()
            {
                isSuccess = false,
                StatusCode = StatusCode.InternalServerError,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() {e.ToString()}
            };
        }
    }
}