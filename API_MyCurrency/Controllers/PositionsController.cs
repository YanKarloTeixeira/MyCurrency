﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_MyCurrency.Models;

namespace API_MyCurrency.Controllers
{
    [Produces("application/json")]
    [Route("api/Positions")]
    public class PositionsController : Controller
    {
        private readonly DBContext _context;

        public PositionsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public IEnumerable<Position> GetPosition()
        {
            return _context.Position;
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _context.Position.SingleOrDefaultAsync(m => m.PositionId == id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // PUT: api/Positions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition([FromRoute] int id, [FromBody] Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != position.PositionId)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        [HttpPost]
        public async Task<IActionResult> PostPosition([FromBody] Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Position.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _context.Position.SingleOrDefaultAsync(m => m.PositionId == id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Position.Remove(position);
            await _context.SaveChangesAsync();

            return Ok(position);
        }

        private bool PositionExists(int id)
        {
            return _context.Position.Any(e => e.PositionId == id);
        }
    }
}