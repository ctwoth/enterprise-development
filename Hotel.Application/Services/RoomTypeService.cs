using Hotel.Application.Contracts.RoomType;
using Hotel.Application.Mappers;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Services;

public class RoomTypeService(IRoomTypeRepository roomTypes) : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypes = roomTypes;

    public async Task<IEnumerable<RoomTypeResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var rooms = await _roomTypes.ReadAll(ct);
        return rooms.Select(rt => RoomTypeMapper.ToDto(rt));
    }

    public async Task<RoomTypeResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var room = await _roomTypes.Read(id, ct);
        return room is null ? null : RoomTypeMapper.ToDto(room);
    }

    public async Task<RoomTypeResponseDto> CreateAsync(RoomTypeCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                Area: double,
                Beds: uint,
                HasShower: bool,
                HasBath: bool,
                Category: RoomCategory,
                DayPrice: double
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var type = RoomTypeMapper.ToEntity(dto);

        var respose = await _roomTypes.Create(type, ct);
        return RoomTypeMapper.ToDto(respose);
    }

    public async Task<RoomTypeResponseDto?> UpdateAsync(int id, RoomTypeCreateUpdateDto dto, CancellationToken ct = default)
    {
        var type = await _roomTypes.Read(id, ct);
        if (type is null) return null;

        if (dto.Area is not null) type.Area = dto.Area.Value;
        if (dto.Beds is not null) type.Beds = dto.Beds.Value;
        if (dto.HasShower is not null) type.HasShower = dto.HasShower.Value;
        if (dto.HasBath is not null) type.HasBath = dto.HasBath.Value;
        if (dto.Category is not null) type.Category = dto.Category.Value;
        if (dto.DayPrice is not null) type.DayPrice = dto.DayPrice.Value;

        await _roomTypes.Update(type, ct);
        return RoomTypeMapper.ToDto(type);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _roomTypes.Delete(id, ct);
}