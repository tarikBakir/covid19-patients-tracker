using System.Collections.Generic;

namespace covid19_patients_tracker.Models.DTOs
{
    public class CovidStatistics
    {
        public int Infected { get; set; }
        public int Healed { get; set; }
        public int Isolated { get; set; }
        public List<CityCovidStats> CityStatistics { get; set; }

    }
}
