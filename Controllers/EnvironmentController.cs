using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnvironmentController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/EnvironmentData
        [HttpPost]
        public async Task<ActionResult<EnvironmentData>> CreateEnvironmentData([FromBody] EnvironmentData environmentData)
        {
            if (environmentData == null)
            {
                return BadRequest("EnvironmentData data is invalid.");
            }

            // Remove the environmentDataId from the request if it's provided (database will auto-generate it)
            environmentData.AssessmentId = 0; // Ensure that EnvironmentDataId is reset before adding it.

            _context.EnvironmentData.Add(environmentData);
            await _context.SaveChangesAsync();

            // Return the created environmentData with the appropriate response
            return CreatedAtAction(nameof(GetEnvironmentData), new { id = environmentData.AssessmentId }, environmentData);
        }


        // GET: api/EnvironmentData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvironmentData>>> GetEnvironmentData()
        {
            var environmentData = await _context.EnvironmentData.ToListAsync();

            if (environmentData == null || !environmentData.Any())
            {
                return NotFound(new { message = "No environmentData found." });
            }

            return Ok(environmentData);
        }



        // PUT: api/EnvironmentData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnvironmentData(int id, [FromBody] EnvironmentData environmentData)
        {
            if (id != environmentData.AssessmentId)
            {
                return BadRequest();
            }

            _context.Entry(environmentData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.EnvironmentData.Any(e => e.AssessmentId == id))
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

        // DELETE: api/EnvironmentData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironmentData(int id)
        {
            var environmentData = await _context.EnvironmentData.FindAsync(id);
            if (environmentData == null)
            {
                return NotFound();
            }

            _context.EnvironmentData.Remove(environmentData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
