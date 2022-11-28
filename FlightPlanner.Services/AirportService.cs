using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlannerNet5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> FindAirport(string search)
        {
            search = search.Trim().ToLower();
            var airport  = _context.Airports.Where(x => x.City.ToLower().Contains(search)
                                                        || x.AirportName.ToLower().Contains(search)
                                                        || x.Country.ToLower().Contains(search)).ToList();

            return airport;
        }
    }
}
