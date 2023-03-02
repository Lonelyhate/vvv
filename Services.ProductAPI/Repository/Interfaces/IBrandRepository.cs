using Services.ProductAPI.Models;

namespace Services.ProductAPI.Repository.Interfaces;

public interface IBrandRepository : IBaseRepository<Brand>
{
    Task<Brand> GetByName(string name);
}