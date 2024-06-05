using Microsoft.AspNetCore.Mvc;
using ToysLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToyRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToysController : ControllerBase
    {
        private readonly ToysRepository _toysRepository;

        public ToysController(ToysRepository toysRepository)
        {
            _toysRepository = toysRepository;
        }

        // GET: api/<ToysController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Toy>> Get([FromQuery] int Price = 0, string? Brand = null)
        {
            IEnumerable<Toy> toys = _toysRepository.GetAll(Price,Brand);
            if(toys == null)
            {
                return BadRequest("Toys collection is null");
            
            }
            else if(!toys.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(toys);
            }
        }

        // GET api/<ToysController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            Toy toy = _toysRepository.GetById(id);
            if(toy == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(toy);
            }
        }

        // POST api/<ToysController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Toy> Post([FromBody] Toy value)
        {
            Toy toy = _toysRepository.Add(value);
            return CreatedAtAction(nameof(Get), new { id = toy.Id }, toy);

        }

        // DELETE api/<ToysController>/5
        [HttpDelete("{id}")]

        public ActionResult<Toy> Delete(int id)
        {
            Toy? toy = _toysRepository.Remove(id);
            if(toy == null)
            {
                return NotFound();            
            }
            return Ok(toy);
        }
    }
}
