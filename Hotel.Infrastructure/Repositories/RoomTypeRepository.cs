using Hotel.Domain.Entities;
using Hotel.Infrastructure;

public class RoomTypeRepository : EfRepository<Booking, int>
{
    public RoomTypeRepository(HotelDbContext context) : base(context) { }
}