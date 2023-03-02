using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.DataBase;
using Services.ProductAPI.Models;
using Services.ProductAPI.Repository.Interfaces;

namespace Services.ProductAPI.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<Category>> GetAll()
    {
        return _db.Categories.ToList();
    }

    public async Task<Category> GetById(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> Create(Category model)
    {
        var category = await _db.Categories.AddAsync(model);
        await _db.SaveChangesAsync();
        return category.Entity;
    }

    public Task<Category> Update(Category model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Category model)
    {
        throw new NotImplementedException();
    }

    public async Task<Category> GetByName(string name)
    {
        var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name == name);
        return category;
    }
}