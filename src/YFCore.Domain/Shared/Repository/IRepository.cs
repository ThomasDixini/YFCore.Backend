namespace YFCore.Domain.Shared.Repository
{
    public interface IRepository<T>
    {
        T GetById(string id);
        ICollection<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
