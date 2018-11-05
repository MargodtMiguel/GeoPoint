using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.ViewModels
{
    public class ScoreVM
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public double TimeSpan { get; set; }
    }
}
