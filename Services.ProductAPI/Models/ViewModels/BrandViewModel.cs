using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models.ViewModels;

public class BrandViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
}