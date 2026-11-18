using Hotel.Domain.Entities;
using Hotel.Infrastructure;


public class RoomRepository : EfRepository<Booking, int>
{
    public RoomRepository(HotelDbContext context) : base(context) { }
}