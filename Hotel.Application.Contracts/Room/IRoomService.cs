namespace Hotel.Application.Contracts.Room;

public interface IRoomService
{
    public Task<IEnumerable<RoomResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<RoomResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<RoomResponseDto> CreateAsync(RoomCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<RoomResponseDto?> UpdateAsync(int id, RoomCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}