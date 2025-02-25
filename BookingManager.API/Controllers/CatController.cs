using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.API.Controllers
{
    [ApiController]
    [Route("Cat")]
    public class CatController : ControllerBase
    {
        static List<string> mockDb = new List<string> { "Miaouss", "Felix", "Garfield", "Duchesse" };

        [HttpGet]
        public IActionResult Get([FromQuery] char letter)
        {
            return Ok(mockDb.Where(c => c.Contains(letter)));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                return Ok(mockDb[id]);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody][MaxLength(25)] string name)
        {
            if (mockDb.Contains(name))
            {
                ModelState.AddModelError("name", "The name must be unique.");
                return BadRequest(ModelState);
            }
            mockDb.Add(name);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody][MaxLength(25)] string name)
        {
            try
            {
                mockDb[id] = name;
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (mockDb[id] != null)
                {
                    mockDb.RemoveAt(id);
                }
                return NoContent();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
