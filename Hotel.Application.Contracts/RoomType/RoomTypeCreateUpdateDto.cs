namespace Hotel.Application.Contracts.RoomType;

using Hotel.Domain.Shared.Enums;

public record RoomTypeCreateUpdateDto(
    double? Area,
    uint? Beds,
    bool? HasBath,
    bool? HasShower,
    double? DayPrice,
    RoomCategory? Category
);