namespace Hotel.Application.Contracts;

public interface IService<TResponseDto, TCreateUpdateDto>
{
    public Task<IEnumerable<TResponseDto>> GetAllAsync(CancellationToken ct = default);
    public Task<TResponseDto?> GetByIdAsync(int id, CancellationToken ct = default);
    public Task<TResponseDto> CreateAsync(TCreateUpdateDto dto, CancellationToken ct = default);
    public Task<TResponseDto?> UpdateAsync(int id, TCreateUpdateDto dto, CancellationToken ct = default);
    public Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}