using Microsoft.AspNetCore.Mvc;
using Zoo.Entities;
using ZooApi.Interface;

namespace ZooApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaretakerController : ControllerBase
    {
        private readonly IService<Caretaker> _service;

        public CaretakerController(IService<Caretaker> service)
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
        public ActionResult Post([FromBody] Caretaker caretaker)
        {
            _service.Create(caretaker);
            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] Caretaker caretaker)
        {
            _service.Update(id, caretaker);
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
