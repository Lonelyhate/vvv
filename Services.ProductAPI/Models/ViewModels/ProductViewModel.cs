using System.Text.Json.Serialization;

namespace Services.ProductAPI.Models.ViewModels;

/// <summary>
/// Представление товара
/// </summary>
public class ProductViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }
    
    /// <summary>
    /// Разамер
    /// </summary>
    [JsonPropertyName("sizes")]
    public string Sizes { get; set; }
    
    /// <summary>
    /// Пол
    /// </summary>
    [JsonPropertyName("gender")]
    public Gender Gender { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Код продукта
    /// </summary>
    [JsonPropertyName("codeProduct")]
    public int CodeProduct { get; set; }
    
    /// <summary>
    /// Изображения
    /// </summary>
    [JsonPropertyName("images")]
    public string Images { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    [JsonPropertyName("dateCreation")]
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    [JsonPropertyName("categoryid")]
    public int CategoryId { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    [JsonPropertyName("category")]
    public virtual CategoryViewModel? Category { get; set; }
    
    [JsonPropertyName("brandid")]
    public int BrandId { get; set; }
    
    [JsonPropertyName("brand")]
    public virtual Brand? Brand { get; set; }
}