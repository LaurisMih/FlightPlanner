using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using FlightPlanner.Net5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlightPlannerNet5.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;

        public CustomerApiController(IFlightService flightService, IAirportService airportService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
            _airportService = airportService;
        }


        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
       {
            
            List<Airport> airports = _airportService.FindAirport(search);
            var airportRequests = _mapper.Map<List<Airport>, List<AirportRequest>>(airports);
            return Ok(airportRequests);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest flightRequest)
        {
            if (!flightRequest.IsValid(flightRequest))
            {
                return BadRequest();
            }

            var flight = _flightService.SearchFlights(flightRequest.From, flightRequest.To, flightRequest.DepartureDate);
            var request = _mapper.Map<List<Flight>, List<FlightRequest>>(flight).ToArray();
            return Ok(new PageResult(request));
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetCompletedFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            var request = _mapper.Map<FlightRequest>(flight);
            return Ok(request);
        }
    }
}