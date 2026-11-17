namespace Hotel.Application.Contracts.Client;

public interface IClientService
{
    public Task<IEnumerable<ClientResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<ClientResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<ClientResponseDto> CreateAsync(ClientCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<ClientResponseDto?> UpdateAsync(int id, ClientCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}