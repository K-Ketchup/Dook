using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dook.ASPCoreWebAPI;
using Dook.Shared.Models;

namespace Dook.ASPCoreWebAPI.Controllers
{
    [Route("api/Restroom")]
    [ApiController]
    public class RestroomController : ControllerBase
    {
        private readonly DookWebAPIContext _context;

        public RestroomController(DookWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Restroom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restroom>>> GetRestrooms()
        {
          if (_context.Restrooms == null)
          {
              return NotFound();
          }
            return await _context.Restrooms.ToListAsync();
        }

        // GET: api/Restroom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restroom>> GetRestroom(int id)
        {
          if (_context.Restrooms == null)
          {
              return NotFound();
          }
            var restroom = await _context.Restrooms.FindAsync(id);

            if (restroom == null)
            {
                return NotFound();
            }

            return restroom;
        }

        // PUT: api/Restroom/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestroom(int id, Restroom restroom)
        {
            if (id != restroom.Id)
            {
                return BadRequest();
            }

            _context.Entry(restroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestroomExists(id))
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

        // POST: api/Restroom
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restroom>> PostRestroom(Restroom restroom)
        {
          if (_context.Restrooms == null)
          {
              return Problem("Entity set 'DookWebAPIContext.Restrooms'  is null.");
          }
            _context.Restrooms.Add(restroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestroom), new { id = restroom.Id }, restroom);
        }

        // DELETE: api/Restroom/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestroom(int id)
        {
            if (_context.Restrooms == null)
            {
                return NotFound();
            }
            var restroom = await _context.Restrooms.FindAsync(id);
            if (restroom == null)
            {
                return NotFound();
            }

            _context.Restrooms.Remove(restroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestroomExists(int id)
        {
            return (_context.Restrooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
