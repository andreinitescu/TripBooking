using TripBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using TripBookingAPI.Interfaces;
using TripBookingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddDbContext<TripContext>(options =>
	options.UseInMemoryDatabase("TripBookingDB"));

builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
