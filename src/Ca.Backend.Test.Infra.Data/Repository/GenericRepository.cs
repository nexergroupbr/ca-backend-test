using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ca.Backend.Test.Infra.Data.Repository;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
    }

    public virtual async Task<IList<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
            throw new NullReferenceException($"Entity with ID {id} not found.");

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
