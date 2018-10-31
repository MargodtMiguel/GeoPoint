using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoPoint.Models
{
    public class Friends
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public bool isPending { get; set; } = true;
        public string UserId { get; set; }
        public string FriendId { get; set; }

        //navigatie properties

        public virtual GeoPointUser User { get; set; }
        public virtual GeoPointUser Friend { get; set; }

    }
}

