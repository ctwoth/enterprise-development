using Hotel.Application.Services;
using Hotel.Application.Contracts.Booking;
using Hotel.Application.Contracts.Client;
using Hotel.Application.Contracts.Room;
using Hotel.Application.Contracts.RoomType;
using Hotel.Domain;
using Hotel.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<HotelDbContext>("hoteldb");

builder.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();