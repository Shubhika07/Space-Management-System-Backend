using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PerformanceDataController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/PerformanceData
        [HttpPost]
        public async Task<ActionResult<MissionPerformance>> CreatePerformanceData([FromBody] MissionPerformance performanceData)
        {
            if (performanceData == null)
            {
                return BadRequest("PerformanceData data is invalid.");
            }

            // Remove the performanceDataId from the request if it's provided (database will auto-generate it)
            performanceData.PerformanceId = 0; // Ensure that PerformanceDataId is reset before adding it.

            _context.MissionPerformance.Add(performanceData);
            await _context.SaveChangesAsync();

            // Return the created performanceData with the appropriate response
            return CreatedAtAction(nameof(GetPerformanceData), new { id = performanceData.PerformanceId }, performanceData);
        }


        // GET: api/PerformanceData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionPerformance>>> GetPerformanceData()
        {
            var performanceData = await _context.MissionPerformance.ToListAsync();

            if (performanceData == null || !performanceData.Any())
            {
                return NotFound(new { message = "No performanceData found." });
            }

            return Ok(performanceData);
        }



        // PUT: api/PerformanceData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformanceData(int id, [FromBody] MissionPerformance performanceData)
        {
            if (id != performanceData.PerformanceId)
            {
                return BadRequest();
            }

            _context.Entry(performanceData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MissionPerformance.Any(e => e.PerformanceId == id))
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

        // DELETE: api/PerformanceData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceData(int id)
        {
            var performanceData = await _context.MissionPerformance.FindAsync(id);
            if (performanceData == null)
            {
                return NotFound();
            }

            _context.MissionPerformance.Remove(performanceData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
