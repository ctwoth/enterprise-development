namespace Hotel.Application.Contracts.Booking;

public interface IBookingService : 
    IService<BookingResponseDto, BookingCreateUpdateDto> { }