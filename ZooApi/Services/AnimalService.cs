using Zoo.Entities;
using ZooApi.Interface;

namespace Zoo.Services
{
    public class AnimalService : IService<Animal>
    {
        private readonly IRepository<Animal> _repository;

        public AnimalService(IRepository<Animal> repository)
        {
            _repository = repository;
        }

        public void Create(Animal entity)
        {
            _repository.Create(entity);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public Animal Read(Guid id)
        {
            var entity = _repository.Read(id);
            return entity;
        }

        public void Update(Guid id, Animal entity)
        {
            _repository.Update(id, entity);
        }
    }
}
