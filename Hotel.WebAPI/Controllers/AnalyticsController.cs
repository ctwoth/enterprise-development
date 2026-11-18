using Hotel.Application.Contracts;
using Hotel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;
/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
/// <param name="analyticsService">Интерфейс службы</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <inheritdoc/>
    [HttpGet("clients-by-room-type/{category}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetClientsByRoomType(RoomCategory category, CancellationToken ct)
        => Ok(await analyticsService.GetClientsByRoomTypeAsync(category, ct));
    /// <inheritdoc/>
    [HttpGet("current-bookings")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetCurrentBookings(CancellationToken ct)
        => Ok(await analyticsService.GetCurrentBookingsAsync(ct));
    /// <inheritdoc/>
    [HttpGet("top5-rooms")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetTop5Rooms(CancellationToken ct)
        => Ok(await analyticsService.GetTop5FrequentlyBookedRoomsAsync(ct));
    /// <inheritdoc/>
    [HttpGet("rooms-booking-count")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetRoomsBookingCount(CancellationToken ct)
        => Ok(await analyticsService.GetBookingCountForEachRoomAsync(ct));
    /// <inheritdoc/>
    [HttpGet("top5-clients")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetTop5Clients(CancellationToken ct)
        => Ok(await analyticsService.GetTop5ClientsByTotalSpentAsync(ct));
}