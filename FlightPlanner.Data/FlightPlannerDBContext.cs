using FlightPlanner.Core.Models;
using FlightPlannerNet5;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightPlanner.Data
{
    public class FlightPlannerDBContext : DbContext, IFlightPlannerDbContext
    {

        public FlightPlannerDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet <Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet <Airport> Airports { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

    }
}
