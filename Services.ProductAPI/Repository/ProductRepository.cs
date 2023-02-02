using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.DataBase;
using Services.ProductAPI.Models;
using Services.ProductAPI.Repository.Interfaces;

namespace Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<Product>> GetAll()
    {
        return _db.Products.Include(p => p.Category).ToList();
    }

    public async Task<Product> GetById(int id)
    {
        var product = _db.Products.Where(p => p.Id == id).Include(p => p.Category).AsNoTracking();
        return product.FirstOrDefault();
    }

    public async Task<Product> Create(Product model)
    {
        var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == model.CategoryId);
        if (category is null)
        {
            _db.AddAsync(model.Category);
            await _db.SaveChangesAsync();
        }

        model.Category = category;
        model.DateCreated = DateTime.Now;
        var product = await _db.Products.AddAsync(model);
        await _db.SaveChangesAsync();
        return product.Entity;
    }

    public async Task<Product> Update(Product model)
    {
        var product = _db.Update(model);
        await _db.SaveChangesAsync();
        return product.Entity;
    }

    public async Task<bool> Delete(Product model)
    {
        _db.Products.Remove(model);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<Product> GetByName(string name)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == name);
        return product;
    }

    public async Task<List<Product>> GetByCategory(string name)
    {
        return _db.Products.Include(p => p.Category).Where(p => p.Category.Name == name).ToList();
    }
}