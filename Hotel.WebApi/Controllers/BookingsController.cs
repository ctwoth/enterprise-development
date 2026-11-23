using Hotel.Application.Contracts.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

/// <summary>
/// Контроллер для CRUD операций над бронированиями
/// </summary>
/// <param name="bookingService">Интерфейс службы</param>
[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    /// <summary>
    /// Получение всех записей в таблице бронирований
    /// </summary>
    /// <returns>Список бронирований</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingResponseDto>), 200)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await bookingService.GetAllAsync(ct));
    
    /// <summary>
    /// Получение записи о брони
    /// </summary>
    /// <param name="id">Id бронирования</param>
    /// <returns>Запись бронирования</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await bookingService.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Создание записи о бронировании
    /// </summary>
    /// <param name="dto">DTO для создания бронирования</param>
    /// <returns>Созданная запись о брони</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookingResponseDto), 201)]
    public async Task<IActionResult> Create(BookingCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await bookingService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
   
    /// <summary>
    /// Обновление записи бронирования
    /// </summary>
    /// <param name="id">Id брони</param>
    /// <param name="dto">DTO для обновления бронирований</param>
    /// <returns>Обновлённая бронь</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BookingResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, BookingCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await bookingService.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }
    
    /// <summary>
    /// Удаление брони
    /// </summary>
    /// <param name="id">Id брони</param>
    /// <returns>Статус-код</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await bookingService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}