using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GeoPoint.API.ViewModels;
using GeoPoint.Models;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeoPoint.API.Controllers
{
    [ApiController]
    //[EnableCors]
    [Produces("application/json")]
    [ApiVersion("0.1")]
    [Authorize]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreRepo _scoreRepo;
        private readonly UserManager<GeoPointUser> _userManager;
        public ScoresController(IScoreRepo scoreRepo, UserManager<GeoPointUser> userManager)
        {
            _scoreRepo = scoreRepo;
            _userManager = userManager;
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

                return BadRequest();
            }
        }

        [HttpPost("api/[controller]/AddScore")]
        public async Task<IActionResult> AddScore(ScoreVM score)
        {
            try
            {
                GeoPointUser user = await _userManager.FindByNameAsync(score.UserName);
                Score s = new Score { Area = score.Area, Value = score.Value, TimeStamp = score.TimeStamp };
                s.Id = Guid.NewGuid().ToString();
                s.UserId = user.Id;
                var oldScore = await _scoreRepo.getOldScore(s);
                if ( oldScore == null)
                {
                    return Ok(await _scoreRepo.CreateScore(s));
                }
                else
                {
                    s.Id = oldScore.Id;
                    return Ok(await _scoreRepo.UpdateScore(s));
                }
            }
            catch (Exception e)
            {
                return BadRequest("Failed to add/update score");
            }
        }
    }
}