using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoPoint.Models;
using GeoPoint.Models.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GeoPoint.API.ViewModels;
using Microsoft.AspNetCore.Cors;

namespace GeoPoint.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GeoPointAPIContext _context;
        private readonly SignInManager<GeoPointUser> _signInMgr;
        private readonly ILogger<AuthController> _logger;

        public AuthController(GeoPointAPIContext context, SignInManager<GeoPointUser> signInMgr, ILogger<AuthController> logger)
        {
            _context = context;
            _signInMgr = signInMgr;
            _logger = logger;
        }

        
        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(IdentityModel identityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Unvalid data");
            try
            {
                var result = await
               _signInMgr.PasswordSignInAsync(identityModel.UserName,
               identityModel.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok("Welcome " + identityModel.UserName);
                }
                ModelState.AddModelError("", "Username or password not found");
                return BadRequest("Failed to login");
            }
            catch (Exception exc)
            {
                _logger.LogError($"Exception thrown when logging in: {exc}");
            }
            return BadRequest("Failed to login");
        }
    }
}