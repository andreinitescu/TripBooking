using TripBookingAPI.Models;
using TripBookingAPI.Models.Dtos;

namespace TripBookingAPI.Interfaces
{
    public interface ITripService
    {
        Task<Trip> CreateTripAsync(Trip trip);
        Task<bool> UpdateTripAsync(int id, Trip trip);
        Task<bool> DeleteTripAsync(int id);
        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<Trip> GetTripByIdAsync(int id);
        Task<IEnumerable<TripDto>> SearchTripsByCountryAsync(string country);
    }
}
