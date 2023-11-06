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

        public Guid Create(Animal entity)
        {
            if (entity is null || entity.Name is null)
            {
                throw new ArgumentNullException();
            }

            var entityInDb = _repository.Read(entity.Id);

            if (entityInDb is not null)
            {
                throw new InvalidOperationException();
            }

            var id = _repository.Create(entity);

            return id;
        }

        public void Delete(Guid id)
        {
            var animal = _repository.Read(id);

            if (animal is null)
            {
                throw new InvalidOperationException();
            }

            _repository.Delete(id);
        }

        public Animal Read(Guid id)
        {
            var entity = _repository.Read(id);
            if (entity is null)
            {
                throw new InvalidOperationException();
            }
            return entity;
        }

        public void Update(Guid id, Animal entity)
        {
            var animalInDb = _repository.Read(id);

            if (animalInDb is null)
            {
                throw new InvalidOperationException();
            }

            if (entity is null || entity.Name is null)
            {
                throw new ArgumentNullException();
            }

            _repository.Update(id, entity);
        }
    }
}
