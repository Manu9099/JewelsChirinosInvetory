// ========================================================
// JewelShrinos.Core/Interfaces
// Contratos sin implementación
// ========================================================
 
using JewelShrinos.Core.Entities;
using System.Linq.Expressions;
 
// =================== REPOSITORY PATTERN =================
 
namespace JewelShrinos.Core.Interfaces
{
    /// <summary>
    /// Interfaz genérica para todas las entidades
    /// Patrón Repository
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        
        Task SaveChangesAsync();
        IQueryable<T> AsQueryable();
    }
}
