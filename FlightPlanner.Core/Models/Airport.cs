using FlightPlanner.Core.Models;
using System.Text.Json.Serialization;

namespace FlightPlannerNet5
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportName { get; set; }
    }
}
