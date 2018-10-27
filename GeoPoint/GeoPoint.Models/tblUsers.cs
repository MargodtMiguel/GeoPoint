using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoPoint.Models
{
    public class tblUsers : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
