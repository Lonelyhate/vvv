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
        return _db.Products.Include(p => p.Category).Include(p => p.Brand).ToList();
    }

    public async Task<Product> GetById(int id)
    {
        var product = _db.Products.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Brand).ToList();
        return product.FirstOrDefault();
    }

    public async Task<Product> Create(Product model)
    {
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

    public async Task<List<Product>> GetByCategory(string[] category)
    {
        var products = new List<Product>();
        foreach (var id in category)
        {
            var currentProduct = _db.Products.Include(p => p.Category).Include(p => p.Brand).Where(p => p.Category.Id == Int32.Parse(id));
            products.AddRange(currentProduct);
        }

        return products;
    }
}