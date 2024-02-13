using Library.Entities;

namespace LibraryAPI.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T>? GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<T> GetSingle(T entity);
    }
}
