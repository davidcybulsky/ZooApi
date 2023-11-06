using AutoMapper;
using Zoo.Entities;
using ZooApi.Dtos;
using ZooApi.Interface;

namespace Zoo.Services
{
    public class CaretakerService : IService<CaretakerDto>
    {

        private readonly IRepository<Caretaker> _repository;
        private readonly IMapper _mapper;

        public CaretakerService(IRepository<Caretaker> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Guid Create(CaretakerDto model)
        {
            var entity = _mapper.Map<Caretaker>(model);
            var id = _repository.Create(entity);
            return id;
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public CaretakerDto Read(Guid id)
        {
            var entity = _repository.Read(id);
            var model = _mapper.Map<CaretakerDto>(entity);
            return model;
        }

        public void Update(Guid id, CaretakerDto model)
        {
            var entity = _mapper.Map<Caretaker>(model);
            _repository.Update(id, entity);
        }
    }
}
