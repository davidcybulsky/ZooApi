using AutoMapper;
using Zoo.Entities;
using ZooApi.Dtos;
using ZooApi.Interface;

namespace Zoo.Services
{
    public class AnimalService : IService<AnimalDto>
    {
        private readonly IRepository<Animal> _repository;
        private readonly IMapper _mapper;

        public AnimalService(IRepository<Animal> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Guid Create(AnimalDto model)
        {
            if (model is null)
            {
                throw new ArgumentNullException();
            }
            var entity = _mapper.Map<Animal>(model);
            var id = _repository.Create(entity);
            return id;
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public AnimalDto Read(Guid id)
        {
            var entity = _repository.Read(id);
            var model = _mapper.Map<AnimalDto>(entity);
            return model;
        }

        public void Update(Guid id, AnimalDto model)
        {
            var animalInDb = _repository.Read(id);

            if (animalInDb is null)
            {
                throw new InvalidOperationException();
            }

            var entity = _mapper.Map<Animal>(model);

            _repository.Update(id, entity);
        }
    }
}
