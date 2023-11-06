namespace ZooApi.Interface
{
    public interface IService<T> where T : class
    {
        Guid Create(T model);
        T Read(Guid id);
        void Update(Guid id, T model);
        void Delete(Guid id);
    }
}
