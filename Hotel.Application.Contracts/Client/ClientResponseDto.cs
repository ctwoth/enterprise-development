namespace Hotel.Application.Contracts.Client;

public record ClientResponseDto(
    int Id,
    string Name,
    string Surname,
    string? Patronymic,
    string PassportNumber,
    DateOnly BirthDate,
    string Citizenship
);