using AxoTourax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxoTourax.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class TouraxController : ControllerBase
    {
        public TouraxController()
        {

        }

        [HttpGet]
        [Route("matiere")]
        public async Task<IActionResult> GetMatieresAsync()
        {
            return Ok("matieres");
        }

        [HttpGet]
        [Route("technique")]
        public async Task<IActionResult> GetTechniquesAsync()
        {
            return Ok("techniques");
        }

        [HttpGet]
        [Route("bobine")]
        public async Task<IActionResult> GetBobinesAsync()
        {
            return Ok("bobines");
        }

        [HttpGet]
        [Route("calcul")]
        public async Task<IActionResult> GetCalculsAsync(string cable, string bobine)
        {
            return Ok("calculs");
        }

        [HttpGet]
        [Route("historique")]
        public async Task<IActionResult> GetHistoriqueAsync()
        {
            return Ok("historique");
        }
    }
}
