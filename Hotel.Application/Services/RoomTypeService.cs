using Hotel.Application.Contracts.RoomType;
using Hotel.Domain;
using Hotel.Domain.Shared.Enums;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRepository<RoomType, int> _roomTypes;
    public RoomTypeService(IRepository<RoomType, int> roomTypes)
    {
        _roomTypes = roomTypes;
    }

    public async Task<IEnumerable<RoomTypeResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var rooms = await _roomTypes.ReadAll(ct);
        return rooms.Select(MapToDto);
    }

    public async Task<RoomTypeResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var room = await _roomTypes.Read(id, ct);
        return room is null ? null : MapToDto(room);
    }

    public async Task<RoomTypeResponseDto> CreateAsync(RoomTypeCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                Area: double area,
                Beds: uint beds,
                HasShower: bool hasShower,
                HasBath: bool hasBath,
                Category: RoomCategory category,
                DayPrice: double dayPrive
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var type = new RoomType
        {
            Area = dto.Area.Value,
            Beds = dto.Beds.Value,
            HasShower = dto.HasShower.Value,
            HasBath = dto.HasBath.Value,
            Category = dto.Category.Value,
            DayPrice = dto.DayPrice.Value
        };

        var respose = await _roomTypes.Create(type, ct);
        return MapToDto(respose);
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
        return MapToDto(type);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _roomTypes.Delete(id, ct);

    private static RoomTypeResponseDto MapToDto(RoomType t) => new(
        t.Id,
        t.Area,
        t.Beds,
        t.HasBath,
        t.HasShower,
        t.DayPrice,
        t.Category
    );
}