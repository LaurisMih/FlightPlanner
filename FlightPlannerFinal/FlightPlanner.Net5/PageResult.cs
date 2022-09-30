using System;

namespace FlightPlannerNet5
{
    public class PageResult
    {

        public int Page { get; set; }
        public int TotalItems { get; set; }
        public Flight[] Items { get; set; }

        public PageResult(Flight[] flights)
        {
            Page = 0;
            TotalItems = flights.Length;
            Items = flights;
        }
    }
}
