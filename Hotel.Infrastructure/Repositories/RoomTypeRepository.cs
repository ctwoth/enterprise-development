using Hotel.Domain.Entities;
using Hotel.Infrastructure;

public class RoomTypeRepository : EfRepository<RoomType, int>, IRoomTypeRepository
{
    public RoomTypeRepository(HotelDbContext context) : base(context) { }
}