using Ca.Backend.Test.Domain.Entities;

namespace Ca.Backend.Test.Infra.Data.Repository.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<IList<T>> GetAllAsync();    
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteByIdAsync(Guid id);
}