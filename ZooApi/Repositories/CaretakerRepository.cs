using Zoo.Entities;
using ZooApi.Data;
using ZooApi.Interface;

namespace ZooApi.Repositories
{
    public class CaretakerRepository : IRepository<Caretaker>
    {
        private readonly ZooContext _db;

        public CaretakerRepository(ZooContext db)
        {
            _db = db;
        }

        public void Create(Caretaker entity)
        {
            _db.Caretakers.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var query = from caretaker in _db.Caretakers
                        where caretaker.Id == id
                        select caretaker;
            var result = query.FirstOrDefault();
            _db.Caretakers.Remove(result);
            _db.SaveChanges();
        }

        public Caretaker Read(Guid id)
        {
            var query = from caretaker in _db.Caretakers
                        where caretaker.Id == id
                        select caretaker;
            return query.FirstOrDefault();
        }

        public void Update(Guid id, Caretaker entity)
        {
            var query = from caretaker in _db.Caretakers
                        where caretaker.Id == id
                        select caretaker;
            var result = query.FirstOrDefault();
            if (result != null)
            {
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                _db.SaveChanges();
            }
        }
    }
}
