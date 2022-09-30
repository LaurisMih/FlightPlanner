using FlightPlanner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerNet5.Controllers
{
    [Route("testing-api/")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear() 
        {
            FlightStorage.Clear();           
            return Ok();
        }    
    }
}
