using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services.ProductAPI.Models;

/// <summary>
/// Таблица Товар
/// </summary>
public class Product
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    [Column("price")]
    public double Price { get; set; }
    
    /// <summary>
    /// Разамер
    /// </summary>
    [Column("size")]
    public string Size { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    [Column("description")]
    public string Description { get; set; }
    
    /// <summary>
    /// Изображения
    /// </summary>
    [Column("images")]
    public string Images { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    [Column]
    public DateTime DateCreated { get; set; }
    
    public int CategoryId { get; set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}