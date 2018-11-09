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
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
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


       
        [HttpGet("api/[controller]/getAllScores")]
        public async Task<IActionResult> GetAllScores()
        {
            try
            {
                return Ok(await _scoreRepo.GetAllScoresAsync());
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception thrown when trying to get all scores: {e}");
                return BadRequest("Failed to get all scores score");
            }
        }

        [HttpGet("api/[controller]/getTopScores")]
        public async Task<IActionResult> getTopScores([Required] string area, int length = 10)
        {
            try
            {
                return Ok(await _scoreRepo.GetTopScoresAsync(area.ToUpper(),length));
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception thrown when trying to get top scores: {e}");
                return BadRequest("Failed to get top scores");
            }
        }

        [HttpPost("api/[Controller]/addScore")]
        public async Task<IActionResult> addScore(ScoreVM scoreVM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Unvalid data");
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                Score score = new Score
                {
                    User = await _userManager.FindByIdAsync(userId),
                    Area = scoreVM.Area.ToUpper(),
                    Value = scoreVM.Value,
                    TimeSpan = scoreVM.TimeSpan,
                    TimeStamp = scoreVM.TimeStamp
                };
                return Ok(await _scoreRepo.CreateAsync(score));
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception thrown when trying to add a score: {e}");
                return BadRequest("Failed to add score");
            }
        }
    }
}