using Microsoft.AspNetCore.Mvc;
using AuthECAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/ReportData
        [HttpPost]
        public async Task<ActionResult<ReportData>> CreateReportData([FromBody] ReportData reportData)
        {
            if (reportData == null)
            {
                return BadRequest("ReportData data is invalid.");
            }

            // Remove the reportDataId from the request if it's provided (database will auto-generate it)
            reportData.ReportId = 0; // Ensure that ReportDataId is reset before adding it.

            _context.ReportData.Add(reportData);
            await _context.SaveChangesAsync();

            // Return the created reportData with the appropriate response
            return CreatedAtAction(nameof(GetReportData), new { id = reportData.ReportId }, reportData);
        }


        // GET: api/ReportData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportData>>> GetReportData()
        {
            var reportData = await _context.ReportData.ToListAsync();

            if (reportData == null || !reportData.Any())
            {
                return NotFound(new { message = "No reportData found." });
            }

            return Ok(reportData);
        }



        // PUT: api/ReportData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportData(int id, [FromBody] ReportData reportData)
        {
            if (id != reportData.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(reportData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ReportData.Any(e => e.ReportId == id))
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

        // DELETE: api/ReportData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportData(int id)
        {
            var reportData = await _context.ReportData.FindAsync(id);
            if (reportData == null)
            {
                return NotFound();
            }

            _context.ReportData.Remove(reportData);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
