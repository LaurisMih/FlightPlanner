using System;

namespace FlightPlanner.Net5.Models
{
    public class PageResult
    {

        public int Page { get; set; }
        public int TotalItems { get; set; }
        public FlightRequest[] Items { get; set; }

        public PageResult(FlightRequest[] flights)
        {
            Page = 0;
            TotalItems = flights.Length;
            Items = flights;
        }
    }
}
