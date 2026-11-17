namespace Hotel.Application.Contracts.RoomType;

using Hotel.Domain.Shared.Enums;

public record RoomTypeCreateUpdateDto(
    double? Area,
    int? Beds,
    bool? HasBath,
    bool? HasShower,
    decimal? DayPrice,
    RoomCategory? Category
);