using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.Models;
using TripBookingAPI.Services;

namespace TripBookingAPI.Tests;
public class RegistrationServiceTests
{
	private RegistrationService _registrationService;
	private TripContext _context;

	private TripContext CreateContext()
	{
		var options = new DbContextOptionsBuilder<TripContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		return new TripContext(options);
	}

	public RegistrationServiceTests()
	{
		_context = CreateContext();
		_registrationService = new RegistrationService(_context);

		// Seed the database
		_context.Registrations.AddRange(
			new Registration { Id = 1, TripId = 1, Email = "test1@example.com" },
			new Registration { Id = 2, TripId = 2, Email = "test2@example.com" }
		);
		_context.SaveChanges();
	}

	[Fact]
	public async Task CreateRegistrationAsync_ShouldAddRegistration()
	{
		// Arrange
		var registration = new Registration { TripId = 1, Email = "test3@example.com" };

		// Act
		var result = await _registrationService.CreateRegistrationAsync(registration);

		// Assert
		Assert.Equal("test3@example.com", result.Email);
		Assert.Contains(_context.Registrations, r => r.Email == "test3@example.com");
	}

	[Fact]
	public async Task CreateRegistrationAsync_ShouldThrowException_WhenEmailAlreadyRegistered()
	{
		// Arrange
		var registration = new Registration { TripId = 1, Email = "test1@example.com" };

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.CreateRegistrationAsync(registration));
	}

	[Fact]
	public async Task GetRegistrationsAsync_ShouldReturnAllRegistrations()
	{
		// Act
		var result = await _registrationService.GetAllRegistrationsAsync();

		// Assert
		Assert.Equal(2, result.Count());
	}

	[Fact]
	public async Task GetRegistrationAsync_ShouldReturnRegistration_WhenRegistrationExists()
	{
		// Act
		var result = await _registrationService.GetRegistrationByIdAsync(1);

		// Assert
		Assert.Equal("test1@example.com", result.Email);
	}

	[Fact]
	public async Task GetRegistrationAsync_ShouldReturnNull_WhenRegistrationDoesNotExist()
	{
		// Act
		var result = await _registrationService.GetRegistrationByIdAsync(99);

		// Assert
		Assert.Null(result);
	}
}