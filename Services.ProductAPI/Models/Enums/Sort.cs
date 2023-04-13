namespace Services.ProductAPI.Models;

public enum Sort
{
    /// <summary>
    /// По умолчанию
    /// </summary>
    Default = 0,
    /// <summary>
    /// По возрастанию цены
    /// </summary>
    AscendingPrice = 1,
    /// <summary>
    /// По убыванию цены
    /// </summary>
    PriceDescending = 2,
    /// <summary>
    /// Сначала старые товары
    /// </summary>
    FirstOldProducts = 3,
    /// <summary>
    /// Сначала новые товары
    /// </summary>
    FirstNewProducts = 4
}