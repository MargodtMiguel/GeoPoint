using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoPoint.API.ViewModels;
using GeoPoint.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GeoPoint.API.Controllers
{
    [ApiController]
    [EnableCors("GeoPoint")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<GeoPointUser> _signInManager;
        private readonly UserManager<GeoPointUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordHasher<GeoPointUser> _hasher;
        public AuthController(SignInManager<GeoPointUser> signInManager, UserManager<GeoPointUser> userManager, IConfiguration configuration, IPasswordHasher<GeoPointUser> hasher, ILogger<AuthController> logger )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hasher = hasher;
            _logger = logger;
            _configuration = configuration;
        }

        #region cookieAuth
        [HttpPost("api/[controller]/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(IdentityModel identityModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Unvalid data");
            try
            {
                if(User.Identity.IsAuthenticated)
                {
                    await _signInManager.SignOutAsync();
                   
                }
                var result = await _signInManager.PasswordSignInAsync(identityModel.UserName, identityModel.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok("Welcome " + identityModel.UserName);
                }
                ModelState.AddModelError("", "Username or password not found");
                return BadRequest("Failed to login");

            }
            catch (Exception e)
            {
                _logger.LogError($"Exception thrown when trying to login: {e}");
            }
            return BadRequest("Failed to login");
        }

        [HttpPost("api/[controller]/Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(IdentityModel identityModel)
        {
            if (!ModelState.IsValid) return BadRequest("Unvalid data");
            try
            {
                var user = new GeoPointUser { Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), UserName = identityModel.UserName };
                if (User.Identity.IsAuthenticated)
                {
                    await _signInManager.SignOutAsync();

                }
                if (await _userManager.FindByNameAsync(user.UserName) == null)
                {
                    var u = await _userManager.CreateAsync(user, identityModel.Password);
                    return await Login(identityModel);
                }
                else
                {
                    return BadRequest("User already exists!");
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception thrown when trying to register: {e}");
                return BadRequest("Failed to create account");
            }
        }
        [HttpPost("api/[controller]/Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Succes");
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception thrown when trying to logout: {e}");
                return BadRequest("Failed to Sign Out");
            }
        }

        [HttpGet("api/[controller]/isLoggedIn")]
        [AllowAnonymous]
        public IActionResult isLoggedIn()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(true);

                }
                else
                {
                    return Ok(false);
                }
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception thrown when checking if user is logged in: {e}");
                return BadRequest("Failed to check if user was logged in");
            }
        }
        #endregion

    }
}