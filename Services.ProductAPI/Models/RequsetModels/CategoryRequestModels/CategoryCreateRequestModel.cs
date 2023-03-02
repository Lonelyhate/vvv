using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models.RequsetModels.CategoryRequestModels;

public class CategoryCreateRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}