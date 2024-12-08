using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MissionController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Mission
        [HttpPost]
        public async Task<ActionResult<Mission>> CreateMission([FromBody] Mission mission)
        {
            if (mission == null)
            {
                return BadRequest("Mission data is invalid.");
            }

            // Remove the missionId from the request if it's provided (database will auto-generate it)
            mission.MissionId = 0; // Ensure that MissionId is reset before adding it.

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            // Return the created mission with the appropriate response
            return CreatedAtAction(nameof(GetMissions), new { id = mission.MissionId }, mission);
        }


        // GET: api/Mission
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            var missions = await _context.Missions.ToListAsync();

            if (missions == null || !missions.Any())
            {
                return NotFound(new { message = "No missions found." });
            }

            return Ok(missions);
        }



        // PUT: api/Mission/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMission(int id, [FromBody] Mission mission)
        {
            if (id != mission.MissionId)
            {
                return BadRequest();
            }

            _context.Entry(mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Missions.Any(e => e.MissionId == id))
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

        // DELETE: api/Mission/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
