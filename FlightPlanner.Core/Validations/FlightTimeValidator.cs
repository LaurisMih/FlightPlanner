using FlightPlannerNet5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Validations
{
    public class FlightTimeValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if(!string.IsNullOrEmpty(flight?.ArrivalTime) &&
                  !string.IsNullOrEmpty(flight?.DepartureTime))
            {
                var arrival = DateTime.Parse(flight.ArrivalTime);
                var departure = DateTime.Parse(flight.DepartureTime);

                return departure < arrival;
            }

            return false;
        }
    }
}
