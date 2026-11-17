namespace Hotel.Application.Contracts.Room;

public record RoomResponseDto(
    int Id,
    uint Number,
    int Floor,
    bool HasBalcony,
    int RoomTypeId,
    string RoomTypeName,
    decimal DayPrice
);