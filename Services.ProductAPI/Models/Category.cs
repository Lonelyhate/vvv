using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ProductAPI.Models;

/// <summary>
/// Таблица Категории
/// </summary>
[Table("Categories")]
public class Category
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
}