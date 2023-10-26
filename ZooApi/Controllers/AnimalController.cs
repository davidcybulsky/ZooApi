using Microsoft.AspNetCore.Mvc;
using Zoo.Entities;
using ZooApi.Interface;

namespace ZooApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IService<Animal> _service;

        public AnimalController(IService<Animal> service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> Get([FromRoute] Guid id)
        {
            var animal = _service.Read(id);
            return StatusCode(200, animal);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Animal animal)
        {
            _service.Create(animal);
            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Animal animal)
        {
            _service.Update(id, animal);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _service.Delete(id);
            return StatusCode(204);
        }
    }
}