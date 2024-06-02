using Microsoft.AspNetCore.Mvc;
using TripBookingAPI.Interfaces;
using TripBookingAPI.Models;

namespace TripBookingAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TripController : ControllerBase
	{
		private readonly ITripService _tripService;

		public TripController(ITripService tripService)
		{
			_tripService = tripService;
		}

		[HttpPost]
		public async Task<ActionResult<Trip>> PostTrip(Trip trip)
		{
			try
			{
				var createdTrip = await _tripService.CreateTripAsync(trip);
				return CreatedAtAction(nameof(GetTrip), new { id = createdTrip.Id }, createdTrip);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutTrip(int id, Trip trip)
		{
			try
			{
				await _tripService.UpdateTripAsync(id, trip);
				return NoContent();
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTrip(int id)
		{
			bool result = await _tripService.DeleteTripAsync(id);
			if (!result)
			{
				return NotFound("Trip not found.");
			}

			return NoContent();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
		{
			return Ok(await _tripService.GetAllTripsAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Trip>> GetTrip(int id)
		{
			var trip = await _tripService.GetTripByIdAsync(id);
			if (trip is null)
			{
				return NotFound("Trip not found.");
			}

			return Ok(trip);
		}

		[HttpGet("search")]
		public async Task<ActionResult<IEnumerable<Trip>>> SearchTrips([FromQuery] string country)
		{
			return Ok(await _tripService.SearchTripsByCountryAsync(country));
		}
	}
}