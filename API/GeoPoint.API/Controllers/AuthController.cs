using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoPoint.API.Services;
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
    [EnableCors("CorsPolicy")]
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



        #region TokenAuth
        [HttpPost("api/[controller]/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(IdentityModel identityModel)
        {
            
            if (!ModelState.IsValid)
                return BadRequest("Unvalid data");
            try
            {
                
                var jwtsvc = new JWTServices<GeoPointUser>(_configuration,_userManager,_hasher,_logger);
                var token = await jwtsvc.GenerateJwtToken(identityModel);
                if(token.GetType() == typeof(IdentityError))
                {
                    IdentityError error = (IdentityError)token;
                    return BadRequest(error.Description);
                }
                return Ok(token);
            }
            catch (Exception e)
            {
                _logger.LogError($"\r\n\r\nError thrown on AuthController - Login method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to login: "+ e + "\r\n\r\n");
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
                var user = new GeoPointUser { SecurityStamp = Guid.NewGuid().ToString(), UserName = identityModel.Username, Email = identityModel.Email };
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
            catch (Exception e)
            {
                _logger.LogError($"\r\n\r\nError thrown on AuthController - Register method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to Register: " + e + "\r\n\r\n");
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
            catch (Exception e)
            {
                _logger.LogError($"\r\n\r\nError thrown on AuthController - Logout method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to Logout: " + e + "\r\n\r\n");
                return BadRequest("Failed to Sign Out");
            }
        }

        #endregion

    }
}