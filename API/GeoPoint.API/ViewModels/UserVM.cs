using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.ViewModels
{
    public class UserVM
    {
        [Required]
        public string username { get; set; }
    }
}
