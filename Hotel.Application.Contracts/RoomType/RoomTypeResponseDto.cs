using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts.RoomType;

public record RoomTypeResponseDto(
    int Id,
    double Area,
    int Beds,
    bool HasBath,
    bool HasShower,
    decimal DayPrice,
    RoomCategory Category
);