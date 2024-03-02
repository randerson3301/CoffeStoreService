using CoffeStore.Common.Seedwork;

namespace CoffeStore.Common.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> AddAsync(T data);
        Task<T?> GetByIdAsync(Guid id);
        Task UpdateAsync(T data);
    }
}
