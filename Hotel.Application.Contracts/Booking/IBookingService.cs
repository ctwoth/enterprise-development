namespace Hotel.Application.Contracts.Booking;

public interface IBookingService
{
    public Task<IEnumerable<BookingResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<BookingResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<BookingResponseDto> CreateAsync(BookingCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<BookingResponseDto?> UpdateAsync(int id, BookingCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}