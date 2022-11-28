using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlannerNet5;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetCompletedFlightById(int id)
        {
            return _context.Flights.Include(x => x.From)
                .Include(x => x.To)
                .SingleOrDefault(X => X.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Any(f => flight.ArrivalTime == f.ArrivalTime
                                      && flight.DepartureTime == f.DepartureTime
                                      && flight.Carrier == f.Carrier
                                      && flight.To.AirportName == f.To.AirportName
                                      && flight.From.AirportName == f.From.AirportName);
        }

        public List<Flight> SearchFlights(string from, string to, string departureDate)
        {
            return _context.Flights.Where(f => f.From.AirportName.Trim().ToLower() == from.Trim().ToLower()
            && f.To.AirportName.Trim().ToLower() == to.Trim().ToLower()
            && f.DepartureTime.Substring(0, 10) == departureDate).ToList();
        }
    }
}
