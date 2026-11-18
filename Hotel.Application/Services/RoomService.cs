using Hotel.Application.Contracts.Room;
using Hotel.Application.Mappers;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

public class RoomService(IRoomRepository _rooms, IRoomTypeRepository _roomTypes) : IRoomService
{
    public async Task<IEnumerable<RoomResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var rooms = await _rooms.ReadAll(ct);
        return rooms.Select(r => RoomMapper.ToDto(r));
    }

    public async Task<RoomResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var room = await _rooms.Read(id, ct);
        return room is null ? null : RoomMapper.ToDto(room);
    }

    public async Task<RoomResponseDto> CreateAsync(RoomCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                Floor: uint,
                HasBalcony: bool,
                Number: uint,
                RoomTypeId: int typeId
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var type = await _roomTypes.Read(typeId, ct);
        if (type is null)
            throw new ArgumentException("Типа комнаты с таким id не существует.");

        var room = RoomMapper.ToEntity(dto, type);

        var response = await _rooms.Create(room, ct);
        return RoomMapper.ToDto(response);
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
        return RoomMapper.ToDto(room);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _rooms.Delete(id, ct);
}