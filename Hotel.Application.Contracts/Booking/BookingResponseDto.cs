namespace Hotel.Application.Contracts.Booking;

public record BookingResponseDto(
    int Id,
    int ClientId,
    string FirstName,
    string LastName,
    string? Patronymic,
    int RoomId,
    uint RoomNumber,
    DateOnly CheckInDate,
    uint DaysOfStay,
    double TotalPrice
);