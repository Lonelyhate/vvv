using Services.ProductAPI.Models;

namespace Services.ProductAPI.Repository.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetByName(string name);

    Task<List<Product>> GetByCategory(string name);
}