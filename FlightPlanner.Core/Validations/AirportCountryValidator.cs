using FlightPlannerNet5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Validations
{
    public class AirportCountryValidator : IAirportValidator
    {
        public bool isValid(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.Country);
        }
    }
}
