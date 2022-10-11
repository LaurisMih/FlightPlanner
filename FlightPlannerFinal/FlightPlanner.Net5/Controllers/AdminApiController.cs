using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlannerNet5.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDBContext _context;
        private static object _admiLock = new object();

        public AdminApiController(FlightPlannerDBContext context)
        {
            _context = context;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {

            lock (_admiLock)
            {

                var flight = _context.Flights.Include(f => f.From).Include(f => f.To).FirstOrDefault(f => f.Id == id);
               
                if (flight == null)
                {
                    return NotFound();
                }

                return Ok(flight);
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(Flight flight)
        {

            lock (_admiLock)
            {


                if (!FlightStorage.IsFlightValid(flight))
                {
                    return BadRequest();
                }
                if (FlightStorage.ExistingFlight(flight, _context))
                {
                    return Conflict();
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();
                // flight = FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_admiLock)
            {


                // FlightStorage.DeleteFlightById(id);
                var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
                if (flight != null)
                {
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }
                return Ok();
            }
        }
    }
}