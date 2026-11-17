using Hotel.Domain;
using Hotel.Domain.Entities;
using Hotel.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class ClientRepository : EfRepository<Client, int>, IClientRepository
{
    public ClientRepository(HotelDbContext context) : base(context) { }
}