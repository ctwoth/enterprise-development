using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.Room;

public record RoomResponseDto(
    int Id,
    uint Number,
    uint Floor,
    bool HasBalcony,
    int RoomTypeId,
    RoomCategory RoomTypeName,
    double DayPrice
);