using LAMAApp.Domain.Entities;

namespace LAMAApp.Domain.Repositories;

public interface IMiembroRepository
{
    Task<IEnumerable<Miembro>> GetAllAsync();
    Task<Miembro?> GetByIdAsync(int id);
    Task<Miembro> AddAsync(Miembro miembro);
    Task<Miembro> UpdateAsync(Miembro miembro);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Miembro>> GetByRangoAsync(string rango);
    Task<IEnumerable<Miembro>> GetByEstatusAsync(string estatus);
}
