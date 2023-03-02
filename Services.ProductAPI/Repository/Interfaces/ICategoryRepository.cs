using Services.ProductAPI.Models;

namespace Services.ProductAPI.Repository.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> GetByName(string name);
}