using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models.RequsetModels.FavoritesRequestModels;

public class FavoritesAddRequestModel
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
}