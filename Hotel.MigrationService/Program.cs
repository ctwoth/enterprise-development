using Hotel.Infrastructure;
using Hotel.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddNpgsqlDbContext<HotelDbContext>("hoteldb");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();