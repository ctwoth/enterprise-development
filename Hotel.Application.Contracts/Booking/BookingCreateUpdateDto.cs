namespace Hotel.Application.Contracts.Booking;

public record BookingCreateUpdateDto(
    int? ClientId,
    int? RoomId,
    DateOnly? CheckInDate,
    uint? DaysOfStay
);