using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerNet5.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {

        private readonly FlightPlannerDBContext _context;

        public CustomerApiController(FlightPlannerDBContext context)
        {
            _context = context;
        }


        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            Airport[] airports = FlightStorage.GetAirport(search, _context);
            return Ok(airports);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest flightRequest)
        {
            if (!flightRequest.IsValid(flightRequest))
            {
                return BadRequest();
            }

            PageResult results = FlightStorage.SearchFlights(flightRequest, _context);
            return Ok(results);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id, _context);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}