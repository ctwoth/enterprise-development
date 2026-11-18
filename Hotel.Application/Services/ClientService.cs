using Hotel.Application.Contracts.Client;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

public class ClientService : IClientService
{
    private readonly IRepository<Client, int> _clients;

    public ClientService(IRepository<Client, int> clients)
    {
        _clients = clients;
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var clients = await _clients.ReadAll(ct);
        return clients.Select(MapToDto);
    }

    public async Task<ClientResponseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var client = await _clients.Read(id, ct);
        return client is null ? null : MapToDto(client);
    }

    public async Task<ClientResponseDto> CreateAsync(ClientCreateUpdateDto dto, CancellationToken ct = default)
    {
        if (dto is not
            {
                Name: string name,
                Surname: string surname,
                BirthDate: DateOnly birthDate,
                PassportNumber: string passportNumber,
                Citizenship: string citizenship
            })
            throw new ArgumentException("Все поля обязательны для заполнения при создании.");

        var client = new Client
        {
            Name = name,
            Surname = surname,
            Patronymic = dto.Patronymic,
            BirthDate = birthDate,
            PassportNumber = passportNumber,
            Citizenship = citizenship
        };

        var respose = await _clients.Create(client, ct);
        return MapToDto(respose);
    }

    public async Task<ClientResponseDto?> UpdateAsync(int id, ClientCreateUpdateDto dto, CancellationToken ct = default)
    {
        var client = await _clients.Read(id, ct);
        if (client is null) return null;

        if (dto.Name is not null) client.Name = dto.Name;
        if (dto.Surname is not null) client.Surname = dto.Surname;
        if (dto.Patronymic is not null) client.Patronymic = dto.Patronymic;
        if (dto.PassportNumber is not null) client.PassportNumber = dto.PassportNumber;
        if (dto.BirthDate is not null) client.BirthDate = dto.BirthDate.Value;
        if (dto.Citizenship is not null) client.Citizenship = dto.Citizenship;

        await _clients.Update(client, ct);
        return MapToDto(client);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => await _clients.Delete(id, ct);

    private static ClientResponseDto MapToDto(Client c) => new(
        c.Id,
        c.Name,
        c.Surname,
        c.Patronymic,
        c.PassportNumber,
        c.BirthDate,
        c.Citizenship
    );
}