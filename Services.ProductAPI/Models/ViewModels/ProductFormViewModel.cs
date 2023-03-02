namespace Services.ProductAPI.Models.ViewModels;

public class ProductFormViewModel
{
    public string Name { get; set; }
    
    public int BrandId { get; set; }
    
    public int CategoryId { get; set; }
    
    public string Sizes { get; set; }
    
    public double Price { get; set; }
    
    public int CodeProduct { get; set; }
    
    public string Description { get; set; }
    
    public List<IFormFile> Images { get; set; }
}