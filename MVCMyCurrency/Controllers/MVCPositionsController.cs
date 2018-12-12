using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMyCurrency.Data;
using MVCMyCurrency.Models;

namespace MVCMyCurrency.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MVCPositionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MVCPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MVCPositions
        [HttpGet]
        public IEnumerable<MVCPosition> GetMVCPosition()
        {
            return _context.MVCPosition;
        }

        // GET: api/MVCPositions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMVCPosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mVCPosition = await _context.MVCPosition.FindAsync(id);

            if (mVCPosition == null)
            {
                return NotFound();
            }

            return Ok(mVCPosition);
        }

        // PUT: api/MVCPositions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMVCPosition([FromRoute] int id, [FromBody] MVCPosition mVCPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mVCPosition.PositionId)
            {
                return BadRequest();
            }

            _context.Entry(mVCPosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MVCPositionExists(id))
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

        // POST: api/MVCPositions
        [HttpPost]
        public async Task<IActionResult> PostMVCPosition([FromBody] MVCPosition mVCPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MVCPosition.Add(mVCPosition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMVCPosition", new { id = mVCPosition.PositionId }, mVCPosition);
        }

        // DELETE: api/MVCPositions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMVCPosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mVCPosition = await _context.MVCPosition.FindAsync(id);
            if (mVCPosition == null)
            {
                return NotFound();
            }

            _context.MVCPosition.Remove(mVCPosition);
            await _context.SaveChangesAsync();

            return Ok(mVCPosition);
        }

        private bool MVCPositionExists(int id)
        {
            return _context.MVCPosition.Any(e => e.PositionId == id);
        }
    }
}