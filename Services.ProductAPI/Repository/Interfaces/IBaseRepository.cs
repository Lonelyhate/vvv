namespace Services.ProductAPI.Repository.Interfaces;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Получить все записи
    /// </summary>
    /// <returns>Возвращаемый ответ</returns>
    Task<List<T>> GetAll();

    /// <summary>
    /// Получить по Id запись
    /// </summary>
    /// <returns>Возвращаемый ответ</returns>
    Task<T> GetById(int id);

    /// <summary>
    /// Создание записи
    /// </summary>
    /// <returns>Возвращаемый ответ</returns>
    Task<T> Create(T model);

    /// <summary>
    /// Обновить запись
    /// </summary>
    /// <returns>Возвращаемый ответ</returns>
    Task<T> Update(T model);

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <returns>Возвращаемый ответ</returns>
    Task<bool> Delete(T model);
}