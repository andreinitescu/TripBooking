using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Data;
using TripBookingAPI.Models;
using TripBookingAPI.Services;

namespace TripBookingAPI.Tests;
public class TripServiceTests
{
	private readonly TripService _tripService;
	private readonly TripContext _context;

	private TripContext CreateContext()
	{
		var options = new DbContextOptionsBuilder<TripContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		return new TripContext(options);
	}

	public TripServiceTests()
	{
		_context = CreateContext();
		_tripService = new TripService(_context);

		// Seed the database
		_context.Trips.AddRange(
			new Trip { Id = 1, Name = "Trip1", Description = "Descritpion1", Country = "Country1", StartDate = DateTime.Now, NumberOfSeats = 10 },
			new Trip { Id = 2, Name = "Trip2", Description = "Descritpion2", Country = "Country2", StartDate = DateTime.Now, NumberOfSeats = 20 }
		);
		_context.SaveChanges();
	}

	[Fact]
	public async Task GetTripsAsync_ShouldReturnAllTrips()
	{
		// Act
		var result = await _tripService.GetAllTripsAsync();

		// Assert
		Assert.Equal(2, result.Count());
	}

	[Fact]
	public async Task CreateTripAsync_ShouldAddTrip()
	{
		// Arrange
		var trip = new Trip { Name = "UniqueTrip", Country = "Country3", Description = "Descritpion3", StartDate = DateTime.Now, NumberOfSeats = 15 };

		// Act
		var result = await _tripService.CreateTripAsync(trip);

		// Assert
		Assert.Equal("UniqueTrip", result.Name);
		Assert.Contains(_context.Trips, t => t.Name == "UniqueTrip");
	}

	[Fact]
	public async Task CreateTripAsync_ShouldThrowException_WhenTripNameNotUnique()
	{
		// Arrange
		var trip = new Trip { Name = "Trip1", Country = "Country3", StartDate = DateTime.Now, NumberOfSeats = 15 };

		// Act & Assert
		await Assert.ThrowsAsync<ArgumentException>(() => _tripService.CreateTripAsync(trip));
	}

	[Fact]
	public async Task GetTripAsync_ShouldReturnTrip_WhenTripExists()
	{
		// Act
		var result = await _tripService.GetTripByIdAsync(1);

		// Assert
		Assert.Equal("Trip1", result.Name);
	}

	[Fact]
	public async Task GetTripAsync_ShouldReturnNull_WhenTripDoesNotExist()
	{
		// Act
		var result = await _tripService.GetTripByIdAsync(99);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async Task UpdateTripAsync_ShouldUpdateTrip()
	{
		// Arrange
		var trip = await _context.Trips.FindAsync(1);
		trip.Name = "UpdatedTrip";

		// Act
		await _tripService.UpdateTripAsync(1, trip);

		// Assert
		var updatedTrip = await _context.Trips.FindAsync(1);
		Assert.Equal("UpdatedTrip", updatedTrip.Name);
	}

	[Fact]
	public async Task DeleteTripAsync_ShouldRemoveTrip()
	{
		// Act
		await _tripService.DeleteTripAsync(1);

		// Assert
		var deletedTrip = await _context.Trips.FindAsync(1);
		Assert.Null(deletedTrip);
	}
}