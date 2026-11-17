using Microsoft.EntityFrameworkCore;
using Hotel.Application.Contracts.Booking;
using Hotel.Domain.Entities;
using Hotel.Infrastructure;

namespace Hotel.Application.Services;

public class BookingService : IBookingService
{
    private readonly HotelDbContext _dbContext;

    public BookingService(HotelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var bookings = await _dbContext.Bookings
            .Include(b => b.Client)
            .Include(b => b.Room)
            .ToListAsync(cancellationToken);

        return bookings.Select(b => new BookingResponseDto(
            b.Id,
            b.Client.Id,
            b.Client.Name,
            b.Client.Surname,
            b.Client.Patronymic,
            b.Room.Id,
            b.Room.Number,
            b.CheckInDate,
            b.DaysOfStay,
            b.TotalPrice
        ));
    }

    public async Task<BookingResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await _dbContext.Bookings
            .Include(b => b.Client)
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (booking is null) return null;

        return new BookingResponseDto(
            booking.Id,
            booking.Client.Id,
            booking.Client.Name,
            booking.Client.Surname,
            booking.Client.Patronymic,
            booking.Room.Id,
            booking.Room.Number,
            booking.CheckInDate,
            booking.DaysOfStay,
            booking.TotalPrice
        );
    }

    public async Task<BookingResponseDto> CreateAsync(BookingCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.ClientId is null || dto.RoomId is null || dto.CheckInDate is null || dto.DaysOfStay is null)
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var client = await _dbContext.Clients.FindAsync(new object[] { dto.ClientId }, cancellationToken);
        var room = await _dbContext.Rooms.FindAsync(new object[] { dto.RoomId }, cancellationToken);

        if(client is null || room is null)
            throw new ArgumentException("Комнаты или клиента с такими id не существует.");

        var booking = new Booking
        {
            Id = 4,
            Client = client,
            Room = room,
            CheckInDate = dto.CheckInDate.Value,
            DaysOfStay = dto.DaysOfStay.Value
        };

        _dbContext.Bookings.Add(booking);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(booking.Id, cancellationToken)
               ?? throw new InvalidOperationException("Не удалось получить созданную бронь.");
    }

    public async Task<BookingResponseDto?> UpdateAsync(int id, BookingCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var booking = await _dbContext.Bookings.FindAsync(new object[] { id }, cancellationToken);
        if (booking is null) return null;
        
        if (dto.ClientId is not null)
        {
            var client = await _dbContext.Clients.FindAsync(new object[] { dto.ClientId }, cancellationToken);
            if (client != null) booking.Client = client;
        }
        if (dto.RoomId is not null) 
        {
            var room = await _dbContext.Rooms.FindAsync(new object[] { dto.RoomId }, cancellationToken);
            if (room != null) booking.Room = room;
        }
        
        if (dto.CheckInDate is not null) booking.CheckInDate = dto.CheckInDate.Value;
        if (dto.DaysOfStay is not null) booking.DaysOfStay = dto.DaysOfStay.Value;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await _dbContext.Bookings.FindAsync(new object[] { id }, cancellationToken);
        if (booking is null) return false;

        _dbContext.Bookings.Remove(booking);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}