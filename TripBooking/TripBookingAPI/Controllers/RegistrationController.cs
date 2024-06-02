using Microsoft.AspNetCore.Mvc;
using TripBookingAPI.Interfaces;
using TripBookingAPI.Models;

namespace TripBookingAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegistrationsController : ControllerBase
	{
		private readonly IRegistrationService _registrationService;

		public RegistrationsController(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		[HttpPost]
		public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
		{
			try
			{
				var createdRegistration = await _registrationService.CreateRegistrationAsync(registration);
				return CreatedAtAction(nameof(GetRegistration), new { id = createdRegistration.Id }, createdRegistration);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
		{
			return Ok(await _registrationService.GetAllRegistrationsAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Registration>> GetRegistration(int id)
		{

			var registration = await _registrationService.GetRegistrationByIdAsync(id);
			if (registration is null)
			{
				return NotFound($"Registration with ID {id} not found.");
			}

			return Ok(registration);
		}
	}
}