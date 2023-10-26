using Zoo.Entities;
using ZooApi.Interface;

namespace Zoo.Services
{
    public class CaretakerService : IService<Caretaker>
    {

        private readonly IRepository<Caretaker> _repository;

        public CaretakerService(IRepository<Caretaker> repository)
        {
            _repository = repository;
        }
        public void Create(Caretaker entity)
        {
            _repository.Create(entity);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public Caretaker Read(Guid id)
        {
            var entity = _repository.Read(id);
            return entity;
        }

        public void Update(Guid id, Caretaker entity)
        {
            _repository.Update(id, entity);
        }
    }
}
