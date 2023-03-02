using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.DataBase;
using Services.ProductAPI.Models;
using Services.ProductAPI.Repository.Interfaces;

namespace Services.ProductAPI.Repository;

public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _db;

    public BrandRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<List<Brand>> GetAll()
    {
        return _db.Brands.ToList();
    }

    public async Task<Brand> GetById(int id)
    {
        return await _db.Brands.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Brand> Create(Brand model)
    {
        var brand = await _db.AddAsync(model);
        await _db.SaveChangesAsync();
        return brand.Entity;
    }

    public Task<Brand> Update(Brand model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Brand model)
    {
        throw new NotImplementedException();
    }

    public async Task<Brand> GetByName(string name)
    {
        return await _db.Brands.FirstOrDefaultAsync(b => b.Name == name);
    }
}