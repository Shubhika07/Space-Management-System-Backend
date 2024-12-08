using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquipmentController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Equipment
        [HttpPost]
        public async Task<ActionResult<Equipment>> CreateEquipment([FromBody] Equipment equipment)
        {
            if (equipment == null)
            {
                return BadRequest("Equipment data is invalid.");
            }

            // Remove the equipmentId from the request if it's provided (database will auto-generate it)
            equipment.EquipmentId = 0; // Ensure that EquipmentId is reset before adding it.

            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            // Return the created equipment with the appropriate response
            return CreatedAtAction(nameof(GetEquipments), new { id = equipment.EquipmentId }, equipment);
        }


        // GET: api/Equipment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipments()
        {
            var equipments = await _context.Equipments.ToListAsync();

            if (equipments == null || !equipments.Any())
            {
                return NotFound(new { message = "No equipments found." });
            }

            return Ok(equipments);
        }



        // PUT: api/Equipment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment(int id, [FromBody] Equipment equipment)
        {
            if (id != equipment.EquipmentId)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Equipments.Any(e => e.EquipmentId == id))
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

        // DELETE: api/Equipment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
