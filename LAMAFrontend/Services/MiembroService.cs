using System.Net.Http.Json;
using LAMAFrontend.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace LAMAFrontend.Services;

public class MiembroService : IMiembroService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MiembroService> _logger;
    private const string ApiEndpoint = "api/Miembros";

    public MiembroService(HttpClient httpClient, ILogger<MiembroService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<MiembroDto>> GetAllMiembrosAsync()
    {
        try
        {
            var miembros = await _httpClient.GetFromJsonAsync<IEnumerable<MiembroDto>>(ApiEndpoint);
            return miembros ?? new List<MiembroDto>();
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return new List<MiembroDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los miembros");
            throw;
        }
    }

    public async Task<MiembroDto?> GetMiembroByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<MiembroDto>($"{ApiEndpoint}/{id}");
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return null;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Miembro con Id {Id} no encontrado", id);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el miembro con Id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<MiembroDto>> GetMiembrosByRangoAsync(string rango)
    {
        try
        {
            var miembros = await _httpClient.GetFromJsonAsync<IEnumerable<MiembroDto>>($"{ApiEndpoint}/rango/{rango}");
            return miembros ?? new List<MiembroDto>();
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return new List<MiembroDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener miembros por rango {Rango}", rango);
            throw;
        }
    }

    public async Task<IEnumerable<MiembroDto>> GetMiembrosByEstatusAsync(string estatus)
    {
        try
        {
            var miembros = await _httpClient.GetFromJsonAsync<IEnumerable<MiembroDto>>($"{ApiEndpoint}/estatus/{estatus}");
            return miembros ?? new List<MiembroDto>();
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return new List<MiembroDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener miembros por estatus {Estatus}", estatus);
            throw;
        }
    }

    public async Task<MiembroDto?> CreateMiembroAsync(CreateMiembroDto miembro)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(ApiEndpoint, miembro);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MiembroDto>();
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el miembro");
            throw;
        }
    }

    public async Task<MiembroDto?> UpdateMiembroAsync(UpdateMiembroDto miembro)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiEndpoint}/{miembro.Id}", miembro);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MiembroDto>();
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el miembro con Id {Id}", miembro.Id);
            throw;
        }
    }

    public async Task<bool> DeleteMiembroAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{ApiEndpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (AccessTokenNotAvailableException ex)
        {
            ex.Redirect();
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el miembro con Id {Id}", id);
            throw;
        }
    }
}
