using AutoMapper;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.ViewModels;

namespace Services.ProductAPI.Services;

public class MappingService
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductViewModel, Product>();
            config.CreateMap<Product, ProductViewModel>();
            config.CreateMap<CategoryViewModel, Category>();
            config.CreateMap<Category, CategoryViewModel>();
        });
        return mappingConfiguration;
    }
}