using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Models
{
    public class Person
    {
        [SwaggerSchema(ReadOnly = true)]
        [Key]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [MaxLength(10)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
