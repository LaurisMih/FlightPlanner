using FlightPlanner.Core.Models;
using FlightPlannerNet5;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Data
{
    public interface IFlightPlannerDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Airport> Airports { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
