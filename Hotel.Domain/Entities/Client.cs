namespace Hotel.Domain.Entities;

/// <summary>
/// Информация о клиенте
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер пасспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string SecondName { get; set; }

    /// <summary>
    /// Отчёство (при наличии)
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождния
    /// </summary>
    public required DateOnly BirthDate { get; set; }

    /// <summary>
    /// Гражданство
    /// </summary>
    public required string Citizenship { get; set; }
}