namespace Hotel.Application.Contracts.Room;

public record RoomCreateUpdateDto(
    uint? Number,
    uint? Floor,
    bool? HasBalcony,
    int? RoomTypeId
);