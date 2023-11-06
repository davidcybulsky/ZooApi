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

        public Guid Create(Caretaker entity)
        {
            if (entity is null)
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
            _repository.Delete(id);
        }

        public Caretaker Read(Guid id)
        {
            var entity = _repository.Read(id);
            return entity;
        }

        public void Update(Guid id, Caretaker entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            _repository.Update(id, entity);
        }
    }
}
