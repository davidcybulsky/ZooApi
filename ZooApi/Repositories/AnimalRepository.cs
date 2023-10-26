using Zoo.Entities;
using ZooApi.Data;
using ZooApi.Interface;

namespace ZooApi.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {
        private readonly ZooContext _db;

        public AnimalRepository(ZooContext db)
        {
            _db = db;
        }

        public void Create(Animal entity)
        {
            _db.Animals.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var query = from animal in _db.Animals
                        where animal.Id == id
                        select animal;
            var result = query.FirstOrDefault();
            _db.Animals.Remove(result);
            _db.SaveChanges();
        }

        public Animal Read(Guid id)
        {
            var query = from animal in _db.Animals
                        where animal.Id == id
                        select animal;
            return query.FirstOrDefault();
        }

        public void Update(Guid id, Animal entity)
        {
            var query = from animal in _db.Animals
                        where animal.Id == id
                        select animal;
            var result = query.FirstOrDefault();
            if (result != null)
            {
                result.Name = entity.Name;
                result.Species = entity.Species;
                result.DateOfBirth = entity.DateOfBirth;
                result.Caretaker = entity.Caretaker;
                _db.SaveChanges();
            }
        }
    }
}
