using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models.RequsetModels.BrandRequestModels;

public class BrandCreateRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}