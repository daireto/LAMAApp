using LAMAFrontend.Models;

namespace LAMAFrontend.Services;

public interface IMiembroService
{
    Task<IEnumerable<MiembroDto>> GetAllMiembrosAsync();
    Task<MiembroDto?> GetMiembroByIdAsync(int id);
    Task<IEnumerable<MiembroDto>> GetMiembrosByRangoAsync(string rango);
    Task<IEnumerable<MiembroDto>> GetMiembrosByEstatusAsync(string estatus);
    Task<MiembroDto?> CreateMiembroAsync(CreateMiembroDto miembro);
    Task<MiembroDto?> UpdateMiembroAsync(UpdateMiembroDto miembro);
    Task<bool> DeleteMiembroAsync(int id);
}
