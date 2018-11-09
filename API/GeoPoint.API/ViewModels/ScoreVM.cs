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
        [CollectionHasElements(ErrorMessage = "Must be EUROPE, SOUTH-AMERICA, NORTH-AMERICA, AFRICA or AUSTRALIA")]
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
                    check.Contains("EUROPE") || 
                    check.Contains("SOUTH-AMERICA") || 
                    check.Contains("NORTH-AMERICA") || 
                    check.Contains("AFRICA") || 
                    check.Contains("AUSTRALIA")
                    );
            }
        }
    }
}
