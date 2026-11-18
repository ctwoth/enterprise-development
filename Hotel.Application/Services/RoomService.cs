using Hotel.Application.Contracts.Room;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRepository<Room, int> _rooms;
    private readonly IRepository<RoomType, int> _roomTypes;
    public RoomService(IRepository<Room, int> rooms, IRepository<RoomType, int> roomTypes)
    {
        _roomTypes = roomTypes;
        _rooms = rooms;
    }

    public async Task<IEnumerable<RoomResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var rooms = await _rooms.ReadAll(ct);
        return rooms.Select(MapToDto);
    }

    public async Task<RoomResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var room = await _rooms.Read(id, ct);
        return room is null ? null : MapToDto(room);
    }

    public async Task<RoomResponseDto> CreateAsync(RoomCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                Floor: uint floor,
                HasBalcony: bool hasBalcony,
                Number: uint number,
                RoomTypeId: int typeId
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var type = await _roomTypes.Read(typeId, ct);
        if (type is null)
            throw new ArgumentException("Типа комнаты с таким id не существует.");

        var room = new Room
        {
            Floor = floor,
            HasBalcony = hasBalcony,
            Number = number,
            Type = type,
        };

        var response = await _rooms.Create(room, ct);
        return MapToDto(response);
    }

    public async Task<RoomResponseDto?> UpdateAsync(int id, RoomCreateUpdateDto dto, CancellationToken ct = default)
    {
        var room = await _rooms.Read(id, ct);
        if (room is null) return null;

        if (dto.Floor is not null) room.Floor = dto.Floor.Value;
        if (dto.Number is not null) room.Number = dto.Number.Value;
        if (dto.HasBalcony is not null) room.HasBalcony = dto.HasBalcony.Value;
        if (dto.RoomTypeId is not null)
        {
            var type = await _roomTypes.Read(dto.RoomTypeId.Value, ct);
            if (type is not null) room.Type = type;
        }

        await _rooms.Update(room, ct);
        return MapToDto(room);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _rooms.Delete(id, ct);

    private static RoomResponseDto MapToDto(Room r) => new(
        r.Id,
        r.Number,
        r.Floor,
        r.HasBalcony,
        r.Type.Id,
        r.Type.Category,
        r.Type.DayPrice
    );
}