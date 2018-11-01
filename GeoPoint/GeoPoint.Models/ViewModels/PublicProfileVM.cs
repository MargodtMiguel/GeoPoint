using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.ViewModels
{
    public class PublicProfileVM
    {
        [Required]
        public string ProfileId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
