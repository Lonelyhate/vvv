using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.DataBase;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.FavoritesResponseModels;
using Services.ProductAPI.Models.RequsetModels.FavoritesRequestModels;
using Services.ProductAPI.Repository.Interfaces;

namespace Services.ProductAPI.Repository;

public class FavoritesRepository : IFavoritesRepository
{
    private readonly ApplicationDbContext _db;

    public FavoritesRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<Favorites> AddToFavorites(Favorites model)
    {
        var favorites = await _db.Favorites.AddAsync(model);
        await _db.SaveChangesAsync();
        return favorites.Entity;
    }

    public async Task<List<Favorites>> GetFavoritesByUserId(int userId)
    {
        var favorites = _db.Favorites.Where(f => f.UserId == userId)
            .Include(f => f.Product)
            .Include(f => f.Product.Brand)
            .Include(f => f.Product.Category);
        return favorites.ToList();
    }

    public async Task<Favorites> GetProductFromFavorites(int userId, int productId)
    {
        var favorites = _db.Favorites.Where(f => f.UserId == userId).Include(f => f.Product);
        return await favorites.FirstOrDefaultAsync(f => f.ProductId == productId);
    }

    public async Task<bool> DeleteFavoritesById(Favorites model)
    {
        try
        {
            _db.Remove(model);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}