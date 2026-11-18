using Hotel.Application.Contracts;
using Hotel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("clients-by-room-type/{category}")]
    public async Task<IActionResult> GetClientsByRoomType(RoomCategory category, CancellationToken ct)
        => Ok(await _analyticsService.GetClientsByRoomTypeAsync(category, ct));

    [HttpGet("current-bookings")]
    public async Task<IActionResult> GetCurrentBookings(CancellationToken ct)
        => Ok(await _analyticsService.GetCurrentBookingsAsync(ct));

    [HttpGet("top5-rooms")]
    public async Task<IActionResult> GetTop5Rooms(CancellationToken ct)
        => Ok(await _analyticsService.GetTop5FrequentlyBookedRoomsAsync(ct));

    [HttpGet("rooms-booking-count")]
    public async Task<IActionResult> GetRoomsBookingCount(CancellationToken ct)
        => Ok(await _analyticsService.GetBookingCountForEachRoomAsync(ct));

    [HttpGet("top5-clients")]
    public async Task<IActionResult> GetTop5Clients(CancellationToken ct)
        => Ok(await _analyticsService.GetTop5ClientsByTotalSpentAsync(ct));
}