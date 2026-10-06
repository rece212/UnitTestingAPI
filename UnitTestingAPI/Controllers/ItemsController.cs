using Microsoft.AspNetCore.Mvc;
using UnitTestingAPI.Models;

namespace UnitTestingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<string> Items = new() { "Laptop", "Keyboard", "Mouse" };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Items);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetbyID(int id)
        {
            if (id <0||id >= Items.Count)
            {
                return NotFound(new { message = "Item not found" });
            }
            return Ok(new { id, name = Items[id] });
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateItemRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { message = "Name cannot be empty" });
            }
            Items.Add(request.Name);
            return CreatedAtAction(nameof(GetbyID), new { id = Items.Count - 1 }, request);

        }
    }
}
