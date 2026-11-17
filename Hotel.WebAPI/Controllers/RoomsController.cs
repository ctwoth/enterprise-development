using Hotel.Application.Contracts.Booking;
using Hotel.Application.Contracts.Room;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await _roomService.GetAllAsync(ct));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _roomService.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoomCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await _roomService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RoomCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await _roomService.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _roomService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}