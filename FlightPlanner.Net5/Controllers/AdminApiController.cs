using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Net5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlannerNet5.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IAirportValidator> _airportValidators;
        private readonly IEnumerable<IFlightValidator> _validators;
        private readonly IMapper _mapper;
        private static object _adminLock = new object();

        public AdminApiController(IFlightService flightService, 
            IEnumerable<IAirportValidator> airportValidators,
            IEnumerable<IFlightValidator> validators, IMapper mapper)
        {
            _flightService = flightService;
            _airportValidators = airportValidators;
            _validators = validators;
            _mapper = mapper;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {

            lock (_adminLock)
            {

                var flight = _flightService.GetCompletedFlightById(id);
               
                if (flight == null)
                {
                    return NotFound();
                }
                var response = _mapper.Map<FlightRequest>(flight);
                return Ok(response);
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(FlightRequest request)
        {

            lock (_adminLock)
            {
                var flight = _mapper.Map<Flight>(request);
                if (!_validators.All(f => f.IsValid(flight)) ||
                    !_airportValidators.All(f => f.isValid(flight?.From)) ||
                    !_airportValidators.All(f => f.isValid(flight?.To)))
                {
                    return BadRequest();
                }

                if (_flightService.Exists(flight))
                {
                    return Conflict();
                }

                var result = _flightService.Create(flight);
                if (result.Success)
                {
                    request = _mapper.Map<FlightRequest>(flight);
                    return Created("", request);                                

                }
                return Problem(result.FormatedErrors);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_adminLock)
            {             
                var flight = _flightService.GetById(id);
                if (flight != null)
                {
                    var result = _flightService.Delete(flight);
                    if (result.Success)
                    {
                        return Ok();
                    }
                    return Problem(result.FormatedErrors);
                }
                return Ok();
            }
        }
    }
}