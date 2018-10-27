using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoPoint.Models
{
    public class tblScores
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Map { get; set; }

    }
}
