using FlightPlannerNet5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetCompletedFlightById(int id);

        bool Exists(Flight flight);

        List<Flight> SearchFlights(string from, string to, string departureDate);
    }
}
