using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.RoomType;

public record RoomTypeResponseDto(
    int Id,
    double Area,
    uint Beds,
    bool HasBath,
    bool HasShower,
    double DayPrice,
    RoomCategory Category
);