namespace Hotel.Application.Contracts.Client;

public record ClientCreateUpdateDto(
    string? Name,
    string? Surname,
    string? Patronymic,
    string? PassportNumber,
    DateOnly? BirthDate,
    string? Citizenship
);