using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.ViewModels
{
    public class ScoreVM
    {
        public int Value { get; set; }
        public string Area { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
