using FlightPlannerNet5;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerNet5
{
    public class FlightPlannerDBContext : DbContext
    {

        public FlightPlannerDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet <Flight> Flights { get; set; }
        public DbSet <Airport> Airports { get; set; }
    }
}
