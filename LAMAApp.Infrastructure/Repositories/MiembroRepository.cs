using LAMAApp.Domain.Entities;
using LAMAApp.Domain.Repositories;
using LAMAApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LAMAApp.Infrastructure.Repositories;

public class MiembroRepository : IMiembroRepository
{
    private readonly LAMAAppDbContext _context;

    public MiembroRepository(LAMAAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Miembro>> GetAllAsync()
    {
        return await _context.Miembros
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Miembro?> GetByIdAsync(int id)
    {
        return await _context.Miembros
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Miembro> AddAsync(Miembro miembro)
    {
        await _context.Miembros.AddAsync(miembro);
        await _context.SaveChangesAsync();
        return miembro;
    }

    public async Task<Miembro> UpdateAsync(Miembro miembro)
    {
        _context.Miembros.Update(miembro);
        await _context.SaveChangesAsync();
        return miembro;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var miembro = await _context.Miembros.FindAsync(id);
        if (miembro == null)
        {
            return false;
        }

        _context.Miembros.Remove(miembro);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Miembros.AnyAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Miembro>> GetByRangoAsync(string rango)
    {
        return await _context.Miembros
            .AsNoTracking()
            .Where(m => m.Rango == rango)
            .ToListAsync();
    }

    public async Task<IEnumerable<Miembro>> GetByEstatusAsync(string estatus)
    {
        return await _context.Miembros
            .AsNoTracking()
            .Where(m => m.Estatus == estatus)
            .ToListAsync();
    }
}
