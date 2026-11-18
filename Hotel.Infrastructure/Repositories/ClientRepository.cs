using Hotel.Domain.Entities;
using Hotel.Infrastructure;

public class ClientRepository : EfRepository<Client, int>, IClientRepository
{
    public ClientRepository(HotelDbContext context) : base(context) { }
}