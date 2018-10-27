using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoPoint.Models;

namespace GeoPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tblScoresController : ControllerBase
    {
        private readonly GeoPointAPIContext _context;

        public tblScoresController(GeoPointAPIContext context)
        {
            _context = context;
        }

        // GET: api/tblScores
        [HttpGet]
        public IEnumerable<tblScores> GettblScores()
        {
            return _context.Scores;
        }

        #region test
        //// GET: api/tblScores/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GettblScores([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var tblScores = await _context.tblScores.FindAsync(id);

        //    if (tblScores == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tblScores);
        //}

        //// PUT: api/tblScores/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PuttblScores([FromRoute] Guid id, [FromBody] tblScores tblScores)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tblScores.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblScores).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!tblScoresExists(id))
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

        //// POST: api/tblScores
        //[HttpPost]
        //public async Task<IActionResult> PosttblScores([FromBody] tblScores tblScores)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.tblScores.Add(tblScores);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GettblScores", new { id = tblScores.ID }, tblScores);
        //}

        //// DELETE: api/tblScores/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletetblScores([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var tblScores = await _context.tblScores.FindAsync(id);
        //    if (tblScores == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.tblScores.Remove(tblScores);
        //    await _context.SaveChangesAsync();

        //    return Ok(tblScores);
        //}

        //private bool tblScoresExists(Guid id)
        //{
        //    return _context.tblScores.Any(e => e.ID == id);
        //}
        #endregion
    }
}