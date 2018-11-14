using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo userRepo;
        private readonly UserManager<GeoPointUser> userManager;
        private readonly ILogger<UsersController> logger;
        public UsersController(IUserRepo userRepo, UserManager<GeoPointUser> userManager, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.userRepo = userRepo;
            this.logger = logger;
        }

        [HttpPost("api/[controller]/sendFriendRequest")]
        public async Task<IActionResult> SendFriendRequest([Required]string username)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                GeoPointUser user = await userManager.FindByIdAsync(userId);
                GeoPointUser friend = await userManager.FindByNameAsync(username);
                bool isFriends1 = await userRepo.checkIfFriends(user, username);
                bool isFriends2 = await userRepo.checkIfFriends(friend, user.UserName);
                if (isFriends1 || isFriends2)
                {
                    return BadRequest("Already friends/request still pending");
                }

                Friend f = new Friend
                {
                    Username = user.UserName
                };
                await userRepo.sendFriendRequest(friend,f);
                return Ok("Friend request sent to " + username);
            }
            catch(Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - SendFriendRequest method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to Send Friend Request: " + e + "\r\n\r\n");
                return BadRequest("Failed to send friend request");
            }
        }
        [HttpGet("api/[controller]/getMyFriends")]
        public async Task<IActionResult> GetMyFriends()
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                GeoPointUser user = await userManager.FindByIdAsync(userId);
                return Ok(user.Friends);
            }
            catch (Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - getMyFriends method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to get My friends: " + e + "\r\n\r\n");
                return BadRequest("Failed to get your friendslist");
            }

        }
    }


}