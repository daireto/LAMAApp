using LAMAApp.Domain.Entities;

namespace LAMAApp.Application.Services;

public interface IMiembroService
{
    Task<IEnumerable<Miembro>> GetAllMiembrosAsync();
    Task<Miembro?> GetMiembroByIdAsync(int id);
    Task<Miembro> CreateMiembroAsync(Miembro miembro);
    Task<Miembro> UpdateMiembroAsync(Miembro miembro);
    Task<bool> DeleteMiembroAsync(int id);
    Task<IEnumerable<Miembro>> GetMiembrosByRangoAsync(string rango);
    Task<IEnumerable<Miembro>> GetMiembrosByEstatusAsync(string estatus);
}
