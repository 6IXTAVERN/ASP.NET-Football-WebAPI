namespace WebAPI.DataAccessLayer.Repositories ;


public interface IBaseRepository<T>
{
    Task Create(T entity);
    
    Task<T?> Delete(string entityId);
    
    Task<T?> Update(T entity);

    Task<T?> GetById(string entityId);
    
    Task<List<T>> GetAll();
}