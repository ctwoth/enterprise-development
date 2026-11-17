namespace Hotel.Domain;

public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    public Task<TEntity> Create(TEntity entity, CancellationToken ct = default);
    public Task<TEntity?> Read(TKey entityId, CancellationToken ct = default);
    public Task<IList<TEntity>> ReadAll(CancellationToken ct = default);
    public Task<TEntity> Update(TEntity entity, CancellationToken ct = default);
    public Task<bool> Delete(TKey entityId, CancellationToken ct = default);
}