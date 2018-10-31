using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GeoPoint.API.ViewModels;
using GeoPoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeoPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<GeoPointUser> _signInManager;
        private readonly UserManager<GeoPointUser> _userManager;
        public AuthController(SignInManager<GeoPointUser> signInManager, UserManager<GeoPointUser> userManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("/SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(IdentityModel identityModel )
        {
            if (!ModelState.IsValid)
                return BadRequest("Unvalid data");
            try
            {
                var result = await
               _signInManager.PasswordSignInAsync(identityModel.UserName,
               
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
                Debug.WriteLine(exc);
            }
            return BadRequest("Failed to login"); 
        }

        [HttpPost("/SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(IdentityModel identityModel)
        {
            if (!ModelState.IsValid) return BadRequest("Unvalid data");
            try
            {
                var user = new GeoPointUser { Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), UserName = identityModel.UserName };
                if (await _userManager.FindByNameAsync(user.UserName) == null)
                {
                    var u = await _userManager.CreateAsync(user, identityModel.Password);
                    return await SignIn(identityModel);
                }
                else
                {
                    return BadRequest("User already exists!");
                }
            }
            catch
            {
                return BadRequest("Failed to create account");
            }
        }
    }
}