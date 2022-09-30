using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlannerNet5
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;
        private static object _locker = new object();

        public static Flight AddFlight(Flight flight)
        {
            lock (_locker)
            {
                flight.Id = _id++;
                _flights.Add(flight);
                return flight;
            }
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(x => x.Id == id);
        }

        public static bool ExistingFlight(Flight flight)
        {
            lock (_locker)
            {
                foreach (Flight f in _flights)
                {
                    if (flight.ArrivalTime == f.ArrivalTime
                        && flight.DepartureTime == f.DepartureTime
                        && flight.Carrier == f.Carrier
                        && flight.To.City == f.To.City
                        && flight.From.City == f.From.City
                        && flight.To.AirportName == f.To.AirportName
                        && flight.From.AirportName == f.From.AirportName
                        && flight.To.Country == f.To.Country
                        && flight.From.Country == f.From.Country)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        public static bool IsFlightValid(Flight flight)
        {
            lock (_locker)
            {                
                 if (flight.From == null
                     || flight.To == null
                     || String.IsNullOrEmpty(flight.From.Country) || String.IsNullOrEmpty(flight.From.City)
                     || String.IsNullOrEmpty(flight.From.AirportName) || String.IsNullOrEmpty(flight.To.Country)
                     || String.IsNullOrEmpty(flight.To.City) || String.IsNullOrEmpty(flight.To.AirportName)
                     || String.IsNullOrEmpty(flight.Carrier) || String.IsNullOrEmpty(flight.DepartureTime)
                     || String.IsNullOrEmpty(flight.ArrivalTime) || flight.From.AirportName.ToLower().Trim() == flight.To.AirportName.ToLower().Trim()
                     || DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime))
                 {
                     return false;
                 }

                 return true;
            }       
        }

        public static void DeleteFlightById(int id)
        {
            lock (_locker)
            {
                var flight = GetFlight(id);

                if (flight != null)
                {
                    _flights.Remove(flight);
                }
            }
        }

        public static Airport[] GetAirport(string search)
        {         
                search = search.ToLower().Trim();
                var fromAirports = _flights.Where(f => f.From.AirportName.ToLower().Trim().Contains(search)
                                                       || f.From.City.ToLower().Trim().Contains(search)
                                                       || f.From.Country.ToLower().Trim().Contains(search))
                    .Select(a => a.From).ToArray();
                var toAirports = _flights.Where(f => f.To.AirportName.ToLower().Trim().Contains(search)
                                                     || f.To.City.ToLower().Trim().Contains(search)
                                                     || f.To.Country.ToLower().Trim().Contains(search))
                    .Select(f => f.To).ToArray();

                return fromAirports.Concat(toAirports).ToArray();                     
        }

        public static PageResult SearchFlights(SearchFlightsRequest request)
        {
            lock (_locker)
            {
                var flights = _flights.Where(x => x.From.AirportName.Trim().ToLower() == request.From.Trim().ToLower()
                                                  && x.To.AirportName.Trim().ToLower() == request.To.Trim().ToLower()
                                                  && x.DepartureTime.Substring(0, 10) ==
                                                  request.DepartureDate).ToArray();
                return new PageResult(flights);
            }
        }
    }
}