using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Requests;
using WebApplication1.Services;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }
        
        [HttpGet("/api/trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            var trips = await _tripService.GetTripAsync();
            return Ok(trips);
        }
        
        [HttpPost("/api.trips/{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, ClientDto clientDto)
        {
            var success = await _tripService.AssignClientToTripAsync(idTrip, clientDto);
            if (!success)
            {
                return BadRequest("Unable to assign client to trip.");
            }

            return NoContent();
        }

    }
}