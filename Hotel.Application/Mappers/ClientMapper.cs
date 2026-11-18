using Hex1b.Surfaces;
using Hotel.Application.Contracts.Booking;
using Hotel.Application.Contracts.Client;
using Hotel.Application.Contracts.Room;
using Hotel.Domain.Entities;
using System.Xml.Linq;

namespace Hotel.Application.Mappers;

public static class ClientMapper
{
    public static ClientResponseDto ToDto(Client c) => new(
        c.Id,
        c.Name,
        c.Surname,
        c.Patronymic,
        c.PassportNumber,
        c.BirthDate,
        c.Citizenship
    );
    public static Client ToEntity(ClientCreateUpdateDto dto) => new()
    {
        Name = dto.Name!,
        Surname = dto.Surname!,
        Patronymic = dto.Patronymic,
        PassportNumber = dto.PassportNumber!,
        BirthDate = dto.BirthDate!.Value,
        Citizenship = dto.Citizenship!
    };
}
