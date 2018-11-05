using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoPoint.API.ViewModels;
using GeoPoint.Models;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoPoint.API.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("GeoPoint")]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsRepo _friendsRepo;
        public FriendsController(IFriendsRepo friendsRepo)
        {
            _friendsRepo = friendsRepo;

        }

        [HttpGet("api/[controller]/FriendList")]
        public async Task<IActionResult> GetFriendsByUserId()
        {
            try
            {
                string UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return Ok(await _friendsRepo.GetFriendsByUserId(UserId));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPost("api/[controller]/SendFriendRequest")]
        public async Task<IActionResult> SendFriendRequest(PublicProfileVM publicProfileVM)
        {
            try
            {
                Friends f = new Friends { Id = Guid.NewGuid().ToString(), UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, FriendId = publicProfileVM.ProfileId };
                if (!(_friendsRepo.FriendExist(f)))
                {                   
                    await _friendsRepo.SendFriendRequest(f);
                    return Ok("Friend request send to: " + publicProfileVM.UserName);
                }
                else
                {
                    return BadRequest("Already friends");
                }
                
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPut("api/[controller]/ConfirmFriendRequest")]
        public async Task<IActionResult> ConfirmFriendRequest(FriendVM friendVM)
        {
            try
            {
                Friends f = new Friends { Id = friendVM.FriendId};
                await _friendsRepo.ConfirmFriends(f);
                return Ok("Friendship confirmed");

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}