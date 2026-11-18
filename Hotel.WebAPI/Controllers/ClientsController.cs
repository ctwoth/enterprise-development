using Hotel.Application.Contracts.Client;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await clientService.GetAllAsync(ct));

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await clientService.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Create(ClientCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await clientService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, ClientCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await clientService.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await clientService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}