namespace YFCore.Domain.Shared.Repository
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
