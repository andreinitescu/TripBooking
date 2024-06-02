using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Models;

namespace TripBookingAPI.Data
{
	public class TripContext : DbContext
	{
		public TripContext(DbContextOptions<TripContext> options) : base(options) { }

		public DbSet<Trip> Trips { get; set; }
		public DbSet<Registration> Registrations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Trip>().HasIndex(t => t.Name).IsUnique();
			modelBuilder.Entity<Registration>()
				.HasIndex(r => new { r.TripId, r.Email })
				.IsUnique();
		}
	}
}
