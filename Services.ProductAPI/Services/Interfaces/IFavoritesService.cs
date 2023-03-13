using Services.ProductAPI.Models.FavoritesResponseModels;
using Services.ProductAPI.Models.RequsetModels.FavoritesRequestModels;

namespace Services.ProductAPI.Services.Interfaces;

public interface IFavoritesService
{
    Task<FavoritesAddResponseModel> AddToFavorites(FavoritesAddRequestModel model, int? userId);

    Task<FavoritesGetByIdResponseModel> GetFavoritesById(int? userId);

    Task<FavoritesDeleteResponseModel> DeleteFavoritesById(int productId, int? userId);

    Task<FavoritesCheckResponseModel> CheckFavorites(int productId, int? userId);
}