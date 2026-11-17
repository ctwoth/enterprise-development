using Hotel.Application.Contracts.Booking;
using Hotel.Application.Contracts.Client;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Contracts;

public record ClientsByRoomTypeDto(
    RoomCategory Category,
    IEnumerable<ClientResponseDto> Clients
);

public record RoomBookingCountDto(
    int RoomId,
    uint RoomNumber,
    int BookingCount
);

public record ClientTotalSpentDto(
    ClientResponseDto Client,
    double TotalSpent
);

public interface IAnalyticsService
{
    public Task<ClientsByRoomTypeDto> GetClientsByRoomTypeAsync(RoomCategory category, CancellationToken ct = default);
    public Task<IEnumerable<BookingResponseDto>> GetCurrentBookingsAsync(CancellationToken ct = default);
    public Task<IEnumerable<RoomBookingCountDto>> GetTop5FrequentlyBookedRoomsAsync(CancellationToken ct = default);
    public Task<IEnumerable<RoomBookingCountDto>> GetBookingCountForEachRoomAsync(CancellationToken ct = default);
    public Task<IEnumerable<ClientTotalSpentDto>> GetTop5ClientsByTotalSpentAsync(CancellationToken ct = default);
}