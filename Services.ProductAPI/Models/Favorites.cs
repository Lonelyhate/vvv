using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ProductAPI.Models;

/// <summary>
/// Таблица избранное
/// </summary>
[Table("Favorites")]
public class Favorites
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Column("userId")]
    public int UserId { get; set; }
    
    /// <summary>
    /// Товар
    /// </summary>
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}