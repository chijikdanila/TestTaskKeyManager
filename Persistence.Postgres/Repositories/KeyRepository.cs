using BLL.Services;
using Contracts.Dtos;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Primitives.Enums;

namespace Persistence.Postgres.Repositories;

public class KeyRepository : IKeyRepository
{
    private readonly AppDbContext _context;

    public KeyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> Create(Key key)
    {
        _context.Keys.Add(key);
        await _context.SaveChangesAsync();
        _context.Entry(key).State = EntityState.Detached;

        return key.Id;
    }

    public async Task Update(Key key)
    {
        _context.Keys.Update(key);
        await _context.SaveChangesAsync();
        _context.Entry(key).State = EntityState.Detached;
    }

    public async Task Delete(long id)
    {
        await _context.Keys
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<Key> GetById(long id)
    {
        var key = await _context.Keys
            .AsNoTracking()
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (key is null)
        {
            throw new Exception($"Ключ с id {id} не найден");
        }

        return key;
    }

    public async Task<List<Key>> GetAll()
    {
        return await _context.Keys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync();
    }
}