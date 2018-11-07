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
        [CollectionHasElements(ErrorMessage = "Must be EU, SA, NA, AF or AU")]
        public string Area { get; set; }
        [Required]
        public double TimeSpan { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [AttributeUsage(AttributeTargets.Property)]
        public class CollectionHasElements : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var check = value as string;
                return (
                    check.Contains("EU") || 
                    check.Contains("SA") || 
                    check.Contains("NA") || 
                    check.Contains("AF") || 
                    check.Contains("AU")
                    );
            }
        }
    }
}
