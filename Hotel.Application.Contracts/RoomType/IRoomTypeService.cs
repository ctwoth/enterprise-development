namespace Hotel.Application.Contracts.RoomType;

public interface IRoomTypeService
{
    public Task<IEnumerable<RoomTypeResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<RoomTypeResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<RoomTypeResponseDto> CreateAsync(RoomTypeCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<RoomTypeResponseDto?> UpdateAsync(int id, RoomTypeCreateUpdateDto dto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}