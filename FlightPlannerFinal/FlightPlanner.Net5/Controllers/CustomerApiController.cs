using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerNet5.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            Airport[] airports = FlightStorage.GetAirport(search);
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

            PageResult results = FlightStorage.SearchFlights(flightRequest);
            return Ok(results);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}