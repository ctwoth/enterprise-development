using Hotel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure;

public class EfRepository<T, TId>(HotelDbContext Context) : IRepository<T, TId>
    where T : class
    where TId : struct
{
    protected readonly DbSet<T> DbSet = Context.Set<T>();

    public virtual async Task<T?> Read(TId id, CancellationToken ct = default)
        => await DbSet.FindAsync(new object?[] { id }, ct);

    public virtual async Task<IList<T>> ReadAll(CancellationToken ct = default)
        => await DbSet.ToListAsync(ct);

    public virtual async Task<T> Create(T entity, CancellationToken ct = default)
    {
        var result = await DbSet.AddAsync(entity, ct);
        await Context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public virtual async Task<T> Update(T entity, CancellationToken ct = default)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public virtual async Task<bool> Delete(TId id, CancellationToken ct = default)
    {
        var entity = await Read(id, ct);
        if (entity is null) return false;
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(ct);
        return true;
    }
}