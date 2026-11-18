using Hotel.Domain.Entities;
using Hotel.Infrastructure;


public class RoomRepository : EfRepository<Room, int>, IRoomRepository
{
    public RoomRepository(HotelDbContext context) : base(context) { }
}