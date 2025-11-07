using LAMAApp.Domain.Entities;
using LAMAApp.Domain.Repositories;

namespace LAMAApp.Application.Services;

public class MiembroService : IMiembroService
{
    private readonly IMiembroRepository _miembroRepository;

    public MiembroService(IMiembroRepository miembroRepository)
    {
        _miembroRepository = miembroRepository;
    }

    public async Task<IEnumerable<Miembro>> GetAllMiembrosAsync()
    {
        return await _miembroRepository.GetAllAsync();
    }

    public async Task<Miembro?> GetMiembroByIdAsync(int id)
    {
        return await _miembroRepository.GetByIdAsync(id);
    }

    public async Task<Miembro> CreateMiembroAsync(Miembro miembro)
    {
        return await _miembroRepository.AddAsync(miembro);
    }

    public async Task<Miembro> UpdateMiembroAsync(Miembro miembro)
    {
        var exists = await _miembroRepository.ExistsAsync(miembro.Id);
        if (!exists)
        {
            throw new KeyNotFoundException($"Miembro con Id {miembro.Id} no encontrado");
        }

        return await _miembroRepository.UpdateAsync(miembro);
    }

    public async Task<bool> DeleteMiembroAsync(int id)
    {
        return await _miembroRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Miembro>> GetMiembrosByRangoAsync(string rango)
    {
        return await _miembroRepository.GetByRangoAsync(rango);
    }

    public async Task<IEnumerable<Miembro>> GetMiembrosByEstatusAsync(string estatus)
    {
        return await _miembroRepository.GetByEstatusAsync(estatus);
    }
}
