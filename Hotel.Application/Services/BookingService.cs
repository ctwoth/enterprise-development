using Hotel.Application.Contracts.Booking;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookings;
    private readonly IRepository<Client, int> _clients;
    private readonly IRepository<Room, int> _rooms;

    public BookingService(
        IBookingRepository bookings,
        IRepository<Client, int> clients,
        IRepository<Room, int> rooms)
    {
        _bookings = bookings;
        _clients = clients;
        _rooms = rooms;
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var bookings = await _bookings.ReadAll(ct);
        return bookings.Select(MapToDto);
    }

    public async Task<BookingResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var booking = await _bookings.Read(id, ct);
        return booking is null ? null : MapToDto(booking);
    }

    public async Task<BookingResponseDto> CreateAsync(BookingCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                ClientId: int clientId,
                RoomId: int roomId,
                CheckInDate: DateOnly checkInDate,
                DaysOfStay: uint daysOfStay
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var client = await _clients.Read(clientId, ct);
        var room = await _rooms.Read(roomId, ct);

        if (client is null || room is null)
            throw new ArgumentException("Комнаты или клиента с такими id не существует.");

        if (await _bookings.HasOverlappingBookingAsync(roomId, checkInDate, daysOfStay, ct))
            throw new InvalidOperationException("На выбранные даты номер уже занят.");

        var booking = new Booking
        {
            Client = client,
            Room = room,
            CheckInDate = dto.CheckInDate.Value,
            DaysOfStay = dto.DaysOfStay.Value
        };

        await _bookings.Create(booking, ct);
        return MapToDto(booking);
    }

    public async Task<BookingResponseDto?> UpdateAsync(int id, BookingCreateUpdateDto dto, CancellationToken ct = default)
    {
        var booking = await _bookings.Read(id, ct);
        if (booking is null) return null;

        if (dto.ClientId is not null)
        {
            var client = await _clients.Read(dto.ClientId.Value, ct);
            if (client is not null) booking.Client = client;
        }

        if (dto.RoomId is not null)
        {
            var room = await _rooms.Read(dto.RoomId.Value, ct);
            if (room is not null) booking.Room = room;
        }

        if (dto.CheckInDate is not null) booking.CheckInDate = dto.CheckInDate.Value;
        if (dto.DaysOfStay is not null) booking.DaysOfStay = dto.DaysOfStay.Value;

        await _bookings.Update(booking, ct);
        return MapToDto(booking);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _bookings.Delete(id, ct);

    private static BookingResponseDto MapToDto(Booking b) => new(
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
    );
}