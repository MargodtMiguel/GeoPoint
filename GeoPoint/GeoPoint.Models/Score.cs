using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoPoint.Models
{
    public class Score
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public int Value { get; set; }
        public string Area { get; set; }
        public double TimeSpan { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public string UserId { get; set; }
        public GeoPointUser User { get; set; }

    }
}
