namespace Hotel.Application.Contracts.Room;

public record RoomCreateUpdateDto(
    uint? Number,
    int? Floor,
    bool? HasBalcony,
    int? RoomTypeId
);