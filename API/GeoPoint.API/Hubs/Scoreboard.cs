using GeoPoint.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.Hubs
{
    public class Scoreboard : Hub
    {
        public async Task SendScore(string score)
        {
            await Clients.All.SendAsync("scoreUpdated",score);
        }
        
    }
}
