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
            if (entity is null || entity.Address is null || entity.FirstName is null || entity.LastName is null)
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
            var entityInDb = _repository.Read(id);

            if (entityInDb is null)
            {
                throw new InvalidOperationException();
            }

            _repository.Delete(id);
        }

        public Caretaker Read(Guid id)
        {
            var entity = _repository.Read(id);

            if (entity is null)
            {
                throw new InvalidOperationException();
            }

            return entity;
        }

        public void Update(Guid id, Caretaker entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            var caretakerInDb = _repository.Read(id);

            if (caretakerInDb is null)
            {
                throw new InvalidOperationException();
            }

            _repository.Update(id, entity);
        }
    }
}
