using AxoTourax.Data;
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
    }
}
