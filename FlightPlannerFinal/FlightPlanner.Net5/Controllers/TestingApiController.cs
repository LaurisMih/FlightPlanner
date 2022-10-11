using FlightPlanner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerNet5.Controllers
{
    [Route("testing-api/")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly FlightPlannerDBContext _context;

        public TestingApiController(FlightPlannerDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear() 
        {
            //FlightStorage.Clear();
            _context.RemoveRange(_context.Flights);
            _context.RemoveRange(_context.Airports);
            _context.SaveChanges();
            return Ok();
        }    
    }
}
