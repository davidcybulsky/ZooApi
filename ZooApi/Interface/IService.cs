using Zoo.Entities;

namespace ZooApi.Interface
{
    public interface IService<T> where T : BaseEntity
    {
        void Create(T entity);
        T Read(Guid id);
        void Update(Guid id, T entity);
        void Delete(Guid id);
    }
}
