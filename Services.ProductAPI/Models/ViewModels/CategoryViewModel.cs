using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models.ViewModels;

/// <summary>
/// Представление Категории
/// </summary>
public class CategoryViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
}