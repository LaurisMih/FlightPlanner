using FlightPlannerNet5;

namespace FlightPlanner.Net5.Models
{
    public class FlightRequest
    {
        public int id { get; set; }
        public AirportRequest From { get; set; }
        public AirportRequest To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
