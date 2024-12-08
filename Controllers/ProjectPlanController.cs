using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectPlanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectPlanController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/ProjectPlan
        [HttpPost]
        public async Task<ActionResult<ProjectPlan>> CreateProjectPlan([FromBody] ProjectPlan projectPlan)
        {
            if (projectPlan == null)
            {
                return BadRequest("ProjectPlan data is invalid.");
            }

            // Remove the projectPlanId from the request if it's provided (database will auto-generate it)
            projectPlan.PlanId = 0; // Ensure that ProjectPlanId is reset before adding it.

            _context.ProjectPlan.Add(projectPlan);
            await _context.SaveChangesAsync();

            // Return the created projectPlan with the appropriate response
            return CreatedAtAction(nameof(GetProjectPlan), new { id = projectPlan.PlanId }, projectPlan);
        }


        // GET: api/ProjectPlan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectPlan>>> GetProjectPlan()
        {
            var projectPlan = await _context.ProjectPlan.ToListAsync();

            if (projectPlan == null || !projectPlan.Any())
            {
                return NotFound(new { message = "No projectPlan found." });
            }

            return Ok(projectPlan);
        }



        // PUT: api/ProjectPlan/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectPlan(int id, [FromBody] ProjectPlan projectPlan)
        {
            if (id != projectPlan.PlanId)
            {
                return BadRequest();
            }

            _context.Entry(projectPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProjectPlan.Any(e => e.PlanId == id))
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

        // DELETE: api/ProjectPlan/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectPlan(int id)
        {
            var projectPlan = await _context.ProjectPlan.FindAsync(id);
            if (projectPlan == null)
            {
                return NotFound();
            }

            _context.ProjectPlan.Remove(projectPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
