using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GeoPoint.Models;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Produces("application/json")]
    [ApiVersion("0.1")]
    // [Authorize]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreRepo _scoreRepo;
        public ScoresController(IScoreRepo scoreRepo)
        {
            _scoreRepo = scoreRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetTopScores([Required]string area, [Required]int length)
        {
            try
            {
                return Ok(await _scoreRepo.GetTopScores(area,length));
            }
            catch (Exception e)
            {
                
                return BadRequest();
            }
        }


    }

}
