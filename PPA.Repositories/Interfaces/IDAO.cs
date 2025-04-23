namespace PPA.Repositories.Interfaces;

public interface IDAO<T> where T : class
{
    Task<IEnumerable<T>?> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T?> FindByIdAsync(int id);
}