using AxoTourax.Data;
using AxoTourax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxoTourax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BobineController : ControllerBase
    {
        private readonly AxoTouraxContext _dbContext;

        public BobineController(AxoTouraxContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllBobinesAsync()
        {
            var data = await _dbContext.Bobines.ToListAsync();
            return Ok(new { data });
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetBobineByIdAsync(int id)
        {
            var data = await _dbContext.Bobines.SingleOrDefaultAsync(bob => bob.IdBobine == id);
            if (data is null) return BadRequest(new { Message = $"Aucune bobine avec l'id {id} n'existe." });
            return Ok(new { data });
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddBobineAsync([FromBody]Bobine bobine)
        {
            if (!ModelState.IsValid) return BadRequest(new { Message = ModelState.GetErrors() });

            var data = await _dbContext.AddAsync(bobine);
            await _dbContext.SaveChangesAsync();

            return Ok(new { data });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateBobineAsync([FromBody]Bobine bobine)
        {
            if (!ModelState.IsValid) return BadRequest(new { Message = ModelState.GetErrors() });

            var bobineDb = await _dbContext.Bobines.SingleOrDefaultAsync(bob => bob.IdBobine == bobine.IdBobine);

            if (bobineDb is null) return BadRequest(new { Message = $"Aucune bobine avec l'id {bobine.IdBobine} n'existe." });

            bobineDb.Reference = bobine.Reference;
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Success" });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bobine = await _dbContext.Bobines.SingleOrDefaultAsync(bob => bob.IdBobine == id);
            if (bobine is null) return BadRequest(new { Message = $"Aucune bobine avec l'id {bobine.IdBobine} n'existe." });

            _dbContext.Remove(bobine);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Deleted successfully" });
        }
    }
}
