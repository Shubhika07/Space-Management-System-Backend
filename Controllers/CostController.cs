using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CostController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/CostData
        [HttpPost]
        public async Task<ActionResult<CostData>> CreateCostData([FromBody] CostData costData)
        {
            if (costData == null)
            {
                return BadRequest("CostData data is invalid.");
            }

            // Remove the costDataId from the request if it's provided (database will auto-generate it)
            costData.CostId = 0; // Ensure that CostDataId is reset before adding it.

            _context.CostData.Add(costData);
            await _context.SaveChangesAsync();

            // Return the created costData with the appropriate response
            return CreatedAtAction(nameof(GetCostData), new { id = costData.CostId }, costData);
        }


        // GET: api/CostData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostData>>> GetCostData()
        {
            var costData = await _context.CostData.ToListAsync();

            if (costData == null || !costData.Any())
            {
                return NotFound(new { message = "No costData found." });
            }

            return Ok(costData);
        }



        // PUT: api/CostData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCostData(int id, [FromBody] CostData costData)
        {
            if (id != costData.CostId)
            {
                return BadRequest();
            }

            _context.Entry(costData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CostData.Any(e => e.CostId == id))
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

        // DELETE: api/CostData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostData(int id)
        {
            var costData = await _context.CostData.FindAsync(id);
            if (costData == null)
            {
                return NotFound();
            }

            _context.CostData.Remove(costData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
