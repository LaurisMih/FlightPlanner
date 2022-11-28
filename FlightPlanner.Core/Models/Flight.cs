using FlightPlanner.Core.Models;

namespace FlightPlannerNet5
{
    public class Flight : Entity
    {
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; } 
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }      
    }
}

