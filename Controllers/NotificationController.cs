using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/NotificationData
        [HttpPost]
        public async Task<ActionResult<NotificationData>> CreateNotificationData([FromBody] NotificationData notificationData)
        {
            if (notificationData == null)
            {
                return BadRequest("NotificationData data is invalid.");
            }

            // Remove the notificationDataId from the request if it's provided (database will auto-generate it)
            notificationData.NotificationId = 0; // Ensure that NotificationDataId is reset before adding it.

            _context.NotificationData.Add(notificationData);
            await _context.SaveChangesAsync();

            // Return the created notificationData with the appropriate response
            return CreatedAtAction(nameof(GetNotificationData), new { id = notificationData.NotificationId }, notificationData);
        }


        // GET: api/NotificationData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationData>>> GetNotificationData()
        {
            var notificationData = await _context.NotificationData.ToListAsync();

            if (notificationData == null || !notificationData.Any())
            {
                return NotFound(new { message = "No notificationData found." });
            }

            return Ok(notificationData);
        }



        // PUT: api/NotificationData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificationData(int id, [FromBody] NotificationData notificationData)
        {
            if (id != notificationData.NotificationId)
            {
                return BadRequest();
            }

            _context.Entry(notificationData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.NotificationData.Any(e => e.NotificationId == id))
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

        // DELETE: api/NotificationData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationData(int id)
        {
            var notificationData = await _context.NotificationData.FindAsync(id);
            if (notificationData == null)
            {
                return NotFound();
            }

            _context.NotificationData.Remove(notificationData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
