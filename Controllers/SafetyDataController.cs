using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafetyDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SafetyDataController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/SafetyData
        [HttpPost]
        public async Task<ActionResult<SafetyData>> CreateSafetyData([FromBody] SafetyData safetyData)
        {
            if (safetyData == null)
            {
                return BadRequest("SafetyData data is invalid.");
            }

            // Remove the safetyDataId from the request if it's provided (database will auto-generate it)
            safetyData.LogId = 0; // Ensure that SafetyDataId is reset before adding it.

            _context.SafetyData.Add(safetyData);
            await _context.SaveChangesAsync();

            // Return the created safetyData with the appropriate response
            return CreatedAtAction(nameof(GetSafetyData), new { id = safetyData.LogId }, safetyData);
        }


        // GET: api/SafetyData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SafetyData>>> GetSafetyData()
        {
            var safetyData = await _context.SafetyData.ToListAsync();

            if (safetyData == null || !safetyData.Any())
            {
                return NotFound(new { message = "No safetyData found." });
            }

            return Ok(safetyData);
        }



        // PUT: api/SafetyData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSafetyData(int id, [FromBody] SafetyData safetyData)
        {
            if (id != safetyData.LogId)
            {
                return BadRequest();
            }

            _context.Entry(safetyData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.SafetyData.Any(e => e.LogId == id))
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

        // DELETE: api/SafetyData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSafetyData(int id)
        {
            var safetyData = await _context.SafetyData.FindAsync(id);
            if (safetyData == null)
            {
                return NotFound();
            }

            _context.SafetyData.Remove(safetyData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
