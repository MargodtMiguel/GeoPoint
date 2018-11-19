using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoPoint.API.ViewModels;
using GeoPoint.Models;
using GeoPoint.Models.Data;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace GeoPoint.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<GeoPointUser> userManager;
        private readonly ILogger<UsersController> logger;
        private readonly GeoPointAPIMongoDBContext context;
        public UsersController(GeoPointAPIMongoDBContext context, UserManager<GeoPointUser> userManager, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("api/[controller]/sendFriendRequest")]
        public async Task<IActionResult> SendFriendRequest(UserVM userVM)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                GeoPointUser user = await userManager.FindByIdAsync(userId);
                GeoPointUser friend = await userManager.FindByNameAsync(userVM.username);
                if(user.Friends != null)
                {
                    foreach (Friend f in user.Friends.ToList())
                    {
                        if (f.Username == friend.UserName)
                        {
                            return BadRequest("Already friends");
                        }
                    }
                }
                if(friend.Friends != null)
                {
                    foreach (Friend f in friend.Friends.ToList())
                    {
                        if (f.Username == user.UserName)
                        {
                            return BadRequest("Already friends");
                        }
                    }
                    
                }
                Friend newFriend = new Friend { Username = user.UserName };
                if(friend.Friends == null)
                {
                    friend.Friends = new List<Friend>();
                }               
                friend.Friends.Add(newFriend);
                await userManager.UpdateAsync(friend);
                return Ok("Friend request sent to " + userVM.username);
            }
            catch(Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - SendFriendRequest method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to Send Friend Request: " + e + "\r\n\r\n");
                return BadRequest("Failed to send friend request");
            }
        }
        [HttpPost("api/[controller]/confirmFriendRequest")]
        public async Task<IActionResult> confirmFriendRequest([Required]string friendUsername)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                GeoPointUser user = await userManager.FindByIdAsync(userId);
                if(user.Friends != null)
                {
                    foreach (Friend f in user.Friends.ToList())
                    {
                        if (f.Username == friendUsername)
                        {
                            f.IsPending = false;
                        }
                    }
                }
               
                await userManager.UpdateAsync(user);
                GeoPointUser friend = await userManager.FindByNameAsync(friendUsername);
                if(friend.Friends == null)
                {
                    friend.Friends = new List<Friend>();
                }
                friend.Friends.Add(new Friend
                {
                    Username = user.UserName,
                    IsPending = false
                });
                await userManager.UpdateAsync(friend);
                return Ok("Your now friends with " + friendUsername);
            }
            catch(Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - confirmFriendRequest method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to confirm Friend Request: " + e + "\r\n\r\n");
                return BadRequest("Failed to confirm friend request");
            }
        }
        [HttpDelete("api/[controller]/declineFriendRequest")]
        public async Task<IActionResult> declineFriendRequest(UserVM userVM)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                GeoPointUser user = await userManager.FindByIdAsync(userId);
                GeoPointUser friend = await userManager.FindByNameAsync(userVM.username);
                if((user.Friends != null))
                {
                    foreach (Friend f in user.Friends.ToList())
                    {
                        if (f.Username == userVM.username)
                        {
                            user.Friends.Remove(f);
                        }
                    }
                }
                await userManager.UpdateAsync(user);
                return Ok("friend request declined!");
            }
            catch(Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - declineFriendRequest method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to decline Friend Request: " + e + "\r\n\r\n");
                return BadRequest("Failed to decline friend request");
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
                if(user.Friends == null)
                {
                    return BadRequest("no friends yet");
                }
                else
                {
                    return Ok(user.Friends);
                }
            }
            catch (Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - getMyFriends method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to get My friends: " + e + "\r\n\r\n");
                return BadRequest("Failed to get your friendslist");
            }

        }
        [HttpDelete("api/[controller]/removeFriend")]
        public async Task<IActionResult> removeFriend([Required] string friendUsername)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                GeoPointUser user = await userManager.FindByIdAsync(userId);
                GeoPointUser friend = await userManager.FindByNameAsync(friendUsername);
                foreach(Friend f in user.Friends.ToList())
                {
                    if(f.Username == friendUsername)
                    {
                        user.Friends.Remove(f);
                    }
                }
                foreach (Friend f in friend.Friends.ToList())
                {
                    if (f.Username == user.UserName)
                    {
                        friend.Friends.Remove(f);
                    }
                }
                await userManager.UpdateAsync(user);
                await userManager.UpdateAsync(friend);
                return Ok("Friend was removed");
            }
            catch (Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - removeFriend method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to remove Friend: " + e + "\r\n\r\n");
                return BadRequest("Failed to remove Friend");
            }

        }
        [HttpGet("api/[controller]/searhUser")]
        public async Task<IActionResult> searchUser([Required]string Username)
        {
            try
            {
                IMongoCollection<GeoPointUser> collection = context.Database.GetCollection<GeoPointUser>("Users");
                IEnumerable<GeoPointUser> users = await collection.Find(x => x.UserName.ToLower().StartsWith(Username.ToLower())).ToListAsync();
                List<string> matches = new List<string>();
                if (users != null)
                {
                    foreach (GeoPointUser u in users.ToList())
                    {
                        matches.Add(u.UserName);
                    }
                    return Ok(matches);
                }
                return BadRequest("No matching users found");
            }
            catch(Exception e)
            {
                logger.LogError($"\r\n\r\nError thrown on UsersController - searchUser method (" + DateTime.UtcNow.ToString() + ") \r\nException thrown when trying to search a user: " + e + "\r\n\r\n");
                return BadRequest("Failed to get search for a user");
            }
        }
    }


}