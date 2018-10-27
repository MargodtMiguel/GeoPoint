using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoPoint.Models
{
    public class tblFriends
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public bool isPending { get; set; }
    }
}
