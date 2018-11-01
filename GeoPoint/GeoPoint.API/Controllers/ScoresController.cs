using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoPoint.API.ViewModels;
using GeoPoint.Models;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeoPoint.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("0.1")]
    [Authorize]
    [EnableCors("GeoPoint")]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreRepo _scoreRepo;
        private readonly UserManager<GeoPointUser> _userManager;
        private readonly ILogger<ScoresController> _logger;
        public ScoresController(IScoreRepo scoreRepo, UserManager<GeoPointUser> userManager, ILogger<ScoresController> logger)
        {
            _scoreRepo = scoreRepo;
            _userManager = userManager;
            _logger = logger;
        }


        [HttpGet("api/[controller]/GetTopScores")]
        public async Task<IActionResult> GetTopScores([Required]string area, [Required]int length = 10)
        {
            try
            {
                return Ok(await _scoreRepo.GetTopScores(area, length));
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception thrown when trying to get top scores: {e}");
                return BadRequest();
            }
        }

        [HttpPost("api/[controller]/AddScore")]
        public async Task<IActionResult> AddScore(ScoreVM score)
        {
            if (score == null)
            {
                throw new ArgumentNullException(nameof(score));
            }

            try
            {
                Score s = new Score { Area = score.Area, Value = score.Value, TimeSpan = score.TimeSpan };
                s.Id = Guid.NewGuid().ToString();
                s.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var oldScore = await _scoreRepo.getOldScore(s);
                if ( oldScore == null)
                {
                    return Ok(await _scoreRepo.CreateScore(s));
                }
                else if(oldScore.Value > s.Value)
                {
                    return Ok("This wasn't your highscore");
                }
                else
                {
                    s.Id = oldScore.Id;
                    await _scoreRepo.UpdateScore(s);
                    return Ok("Succes");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception thrown when trying to add or update score: {e}");
                return BadRequest("Failed to add/update score");
            }
        }
    }
}