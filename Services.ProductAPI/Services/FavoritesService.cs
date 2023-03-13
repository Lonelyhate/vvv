using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.FavoritesResponseModels;
using Services.ProductAPI.Models.RequsetModels.FavoritesRequestModels;
using Services.ProductAPI.Models.ViewModels;
using Services.ProductAPI.Repository.Interfaces;
using Services.ProductAPI.Services.Interfaces;

namespace Services.ProductAPI.Services;

public class FavoritesService : IFavoritesService
{
    private readonly IFavoritesRepository _favoritesRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public FavoritesService(IFavoritesRepository favoritesRepository, IMapper mapper, IProductRepository productRepository)
    {
        _favoritesRepository = favoritesRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<FavoritesAddResponseModel> AddToFavorites(FavoritesAddRequestModel model, int userId)
    {
        try
        {
            var response = new FavoritesAddResponseModel();
            var product = await _productRepository.GetById(model.ProductId);
            if (product is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Такого товара нет";
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            var favorites =  await _favoritesRepository.GetFavoritesByUserId(userId);
            if (favorites.Any(f => f.ProductId == model.ProductId))
            {
                response.isSuccess = false;
                response.DisplayMessage = "Товар уже в избарнном";
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            var favorite = await _favoritesRepository.AddToFavorites(new Favorites
            {
                UserId = userId, 
                Product = product, 
                ProductId = product.Id
            });

            response.StatusCode = StatusCode.Created;
            response.Data = _mapper.Map<ProductViewModel>(favorite.Product);
            return response;
        }
        catch (Exception e)
        {
            return new FavoritesAddResponseModel
            {
                StatusCode = StatusCode.InternalServerError,
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() { e.ToString() }
            };
        }
    }

    public async Task<FavoritesGetByIdResponseModel> GetFavoritesById(int userId)
    {
        try
        {
            var response = new FavoritesGetByIdResponseModel();

            var favorites = await _favoritesRepository.GetFavoritesByUserId(userId);
            Console.WriteLine(favorites.Count);
            response.Data = _mapper.Map<List<ProductViewModel>>(favorites.Select(f => f.Product));
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new FavoritesGetByIdResponseModel
            {
                StatusCode = StatusCode.InternalServerError,
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() { e.ToString() }
            };
        }
    }

    public async Task<FavoritesDeleteResponseModel> DeleteFavoritesById(int productId, int userId)
    {
        try
        {
            var response = new FavoritesDeleteResponseModel();

            var favorites = await _favoritesRepository.GetFavoritesByUserId(userId);
            if (favorites is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Избранные не найдены";
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            var favorite = favorites.Where(f => f.ProductId == productId).FirstOrDefault();
            if (favorite is null)
            {
                response.isSuccess = false;
                response.DisplayMessage = "Товар не найден";
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            if (!await _favoritesRepository.DeleteFavoritesById(favorite))
            {
                response.Data = false;
            }
            else
            {
                response.Data = true;
            }

            response.DisplayMessage = "Удалено";
            response.StatusCode = StatusCode.OK;
            return response;
        }
        catch (Exception e)
        {
            return new FavoritesDeleteResponseModel
            {
                StatusCode = StatusCode.InternalServerError,
                isSuccess = false,
                DisplayMessage = "Ошибка сервера",
                ErrorMessage = new List<string>() { e.ToString() }
            };
        }
    }
}