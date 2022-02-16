using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace covid19_patients_tracker.Models
{
    public class Address
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int ApartmentNumber { get; set; }
        public int HouseNumber { get; set; }
    }
}
