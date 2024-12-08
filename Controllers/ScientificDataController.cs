using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScientificDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScientificDataController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/ScientificData
        [HttpPost]
        public async Task<ActionResult<ScientificData>> CreateScientificData([FromBody] ScientificData scientificData)
        {
            if (scientificData == null)
            {
                return BadRequest("ScientificData data is invalid.");
            }

            // Remove the scientificDataId from the request if it's provided (database will auto-generate it)
            scientificData.DataId = 0; // Ensure that ScientificDataId is reset before adding it.

            _context.ScientificData.Add(scientificData);
            await _context.SaveChangesAsync();

            // Return the created scientificData with the appropriate response
            return CreatedAtAction(nameof(GetScientificData), new { id = scientificData.DataId }, scientificData);
        }


        // GET: api/ScientificData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScientificData>>> GetScientificData()
        {
            var scientificData = await _context.ScientificData.ToListAsync();

            if (scientificData == null || !scientificData.Any())
            {
                return NotFound(new { message = "No scientificData found." });
            }

            return Ok(scientificData);
        }



        // PUT: api/ScientificData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScientificData(int id, [FromBody] ScientificData scientificData)
        {
            if (id != scientificData.DataId)
            {
                return BadRequest();
            }

            _context.Entry(scientificData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ScientificData.Any(e => e.DataId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ScientificData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScientificData(int id)
        {
            var scientificData = await _context.ScientificData.FindAsync(id);
            if (scientificData == null)
            {
                return NotFound();
            }

            _context.ScientificData.Remove(scientificData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
