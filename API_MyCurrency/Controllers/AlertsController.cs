using System;
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
    [Route("api/Alerts")]
    public class AlertsController : ControllerBase
    {
        private readonly DBContext _context;

        public AlertsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Alerts
        [HttpGet]
        //public IEnumerable<Alert> GetAlert()
        //{
        //    return _context.Alert;
        //}
        //[HttpGet]
        [HttpGet("{param}")]
        public IEnumerable<Alert> GetAlert([FromRoute]string param)
        {
            int id = 0;
            if (param != null && !String.IsNullOrEmpty(param))
            {
                if (int.TryParse(param, out id))
                {
                    IEnumerable<Alert> alerts = from a in _context.Alert where a.AlertId == id select a;
                    return alerts;
                }
                else
                {
                    IEnumerable<Alert> alerts = from a in _context.Alert where a.Email == param select a;
                    return alerts;
                }
            }
            return _context.Alert;
        }

        // GET: api/Alerts/5
        [ActionName("AlertById")]
        //[HttpGet("{id}")]
        public async Task<IActionResult> GetAlert([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alert = await _context.Alert.FindAsync(id);

            if (alert == null)
            {
                return NotFound();
            }

            return Ok(alert);
        }

        // PUT: api/Alerts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlert([FromRoute] int id, [FromBody] Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alert.AlertId)
            {
                return BadRequest();
            }

            _context.Entry(alert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
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

        // POST: api/Alerts
        [HttpPost]
        public async Task<IActionResult> PostAlert([FromBody] Alert alert)
        {
            if (!ModelState.IsValid || AlertProfileExists(alert))
            {
                return BadRequest(ModelState);
            }

            if (alert.AlertId == 0)
            {
                _context.Alert.Add(alert);
            }
            else
            {
                _context.Entry(alert).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlert", new { id = alert.AlertId }, alert);
        }

        // DELETE: api/Alerts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alert = await _context.Alert.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            _context.Alert.Remove(alert);
            await _context.SaveChangesAsync();

            return Ok(alert);
        }

        private bool AlertExists(int id)
        {
            return _context.Alert.Any(e => e.AlertId == id);
        }
        private bool AlertProfileExists(Alert alert)
        {
            var v = _context.Alert.Any(e => e.CurrencyName == alert.CurrencyName && e.Email == alert.Email);
            return _context.Alert.Any(e => e.CurrencyName == alert.CurrencyName && e.Email == alert.Email);
        }

        //private readonly DBContext _context;

        //public AlertsController(DBContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Alerts
        //[HttpGet]
        //public IEnumerable<Alert> GetAlert()
        //{
        //    return _context.Alert;
        //}

        //// GET: api/Alerts/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAlert([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var alert = await _context.Alert.SingleOrDefaultAsync(m => m.AlertId == id);

        //    if (alert == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(alert);
        //}

        //// PUT: api/Alerts/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAlert([FromRoute] int id, [FromBody] Alert alert)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != alert.AlertId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(alert).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AlertExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Alerts
        //[HttpPost]
        //public async Task<IActionResult> PostAlert([FromBody] Alert alert)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Alert.Add(alert);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAlert", new { id = alert.AlertId }, alert);
        //}

        //// DELETE: api/Alerts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAlert([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var alert = await _context.Alert.SingleOrDefaultAsync(m => m.AlertId == id);
        //    if (alert == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Alert.Remove(alert);
        //    await _context.SaveChangesAsync();

        //    return Ok(alert);
        //}

        //private bool AlertExists(int id)
        //{
        //    return _context.Alert.Any(e => e.AlertId == id);
        //}
    }
}