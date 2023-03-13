using Services.ProductAPI.Models;
using Services.ProductAPI.Models.FavoritesResponseModels;
using Services.ProductAPI.Models.RequsetModels.FavoritesRequestModels;

namespace Services.ProductAPI.Repository.Interfaces;

public interface IFavoritesRepository
{
    Task<Favorites> AddToFavorites(Favorites model);

    Task<List<Favorites>> GetFavoritesByUserId(int userId);

    Task<bool> DeleteFavoritesById(Favorites model);
}