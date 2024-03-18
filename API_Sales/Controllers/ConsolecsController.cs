using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Sales.Modèle; // Assurez-vous que le namespace correspond à vos modèles
using API_Sales.Data; // Assurez-vous que le namespace correspond à votre contexte de données
// controller console
namespace API_Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolecsController : ControllerBase
    {
        private readonly API_SalesContext _context;

        public ConsolecsController(API_SalesContext context)
        {
            _context = context;
        }

        // GET: api/Consolecs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consolec>>> GetConsolecs()
        {
            if (_context.Consolec == null)
            {
                return NotFound();
            }
            return await _context.Consolec.ToListAsync();
        }

        // GET: api/Consolecs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consolec>> GetConsolec(int id)
        {
            if (_context.Consolec == null)
            {
                return NotFound();
            }
            var consolec = await _context.Consolec.FindAsync(id);

            if (consolec == null)
            {
                return NotFound();
            }

            return consolec;
        }

        // PUT: api/Consolecs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsolec(int id, Consolec consolec)
        {
            if (id != consolec.ConsoleId)
            {
                return BadRequest();
            }

            _context.Entry(consolec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsolecExists(id))
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

        // POST: api/Consolecs
        [HttpPost]
        public async Task<ActionResult<Consolec>> PostConsolec(Consolec consolec)
        {
            if (_context.Consolec == null)
            {
                return Problem("Entity set 'API_SalesContext.Consolec' is null.");
            }
            _context.Consolec.Add(consolec);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsolec), new { id = consolec.ConsoleId }, consolec);
        }

        // DELETE: api/Consolecs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsolec(int id)
        {
            if (_context.Consolec == null)
            {
                return NotFound();
            }
            var consolec = await _context.Consolec.FindAsync(id);
            if (consolec == null)
            {
                return NotFound();
            }

            _context.Consolec.Remove(consolec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsolecExists(int id)
        {
            return (_context.Consolec?.Any(e => e.ConsoleId == id)).GetValueOrDefault();
        }
    }
}
