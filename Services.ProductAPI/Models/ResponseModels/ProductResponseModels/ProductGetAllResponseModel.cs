using System.Text.Json.Serialization;
using Services.ProductAPI.Models.ViewModels;

namespace Services.ProductAPI.Models;

public class ProductGetAllModel
{
    [JsonPropertyName("price_min")]
    public int PriceMin { get; set; }
    
    [JsonPropertyName("price_max")]
    public int PriceMax { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("products")]
    public List<ProductViewModel> Products { get; set; }
}

public class ProductGetAllResponseModel : BaseResponse<ProductGetAllModel>
{
    
}

