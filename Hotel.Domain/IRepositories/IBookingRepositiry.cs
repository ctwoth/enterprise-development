using Hotel.Domain;
using Hotel.Domain.Entities;

public interface IBookingRepository : IRepository<Booking, int>
{
    public Task<bool> HasOverlappingBookingAsync(int roomId, DateOnly checkIn, uint days, CancellationToken ct = default);
}