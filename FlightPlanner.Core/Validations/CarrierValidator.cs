using FlightPlannerNet5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Validations
{
    public class CarrierValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.Carrier);

        }
    }
}
