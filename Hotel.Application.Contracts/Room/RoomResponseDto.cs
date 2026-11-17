namespace Hotel.Application.Contracts.Room;

public record RoomResponseDto(
    int Id,
    uint Number,
    uint Floor,
    bool HasBalcony,
    int RoomTypeId,
    string RoomTypeName,
    double DayPrice
);