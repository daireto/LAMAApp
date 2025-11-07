using LAMAApp.Application.DTOs;
using LAMAApp.Application.Services;
using LAMAApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAMAApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MiembrosController : ControllerBase
{
    private readonly IMiembroService _miembroService;
    private readonly ILogger<MiembrosController> _logger;

    public MiembrosController(IMiembroService miembroService, ILogger<MiembrosController> logger)
    {
        _miembroService = miembroService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Policy = "AllUsers")]
    public async Task<ActionResult<IEnumerable<MiembroDto>>> GetAll()
    {
        try
        {
            var miembros = await _miembroService.GetAllMiembrosAsync();
            var miembrosDto = miembros.Select(MapToDto);
            return Ok(miembrosDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los miembros");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AllUsers")]
    public async Task<ActionResult<MiembroDto>> GetById(int id)
    {
        try
        {
            var miembro = await _miembroService.GetMiembroByIdAsync(id);
            if (miembro == null)
            {
                return NotFound($"Miembro con Id {id} no encontrado");
            }
            return Ok(MapToDto(miembro));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el miembro con Id {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("rango/{rango}")]
    [Authorize(Policy = "AllUsers")]
    public async Task<ActionResult<IEnumerable<MiembroDto>>> GetByRango(string rango)
    {
        try
        {
            var miembros = await _miembroService.GetMiembrosByRangoAsync(rango);
            var miembrosDto = miembros.Select(MapToDto);
            return Ok(miembrosDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener miembros por rango {Rango}", rango);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("estatus/{estatus}")]
    [Authorize(Policy = "AllUsers")]
    public async Task<ActionResult<IEnumerable<MiembroDto>>> GetByEstatus(string estatus)
    {
        try
        {
            var miembros = await _miembroService.GetMiembrosByEstatusAsync(estatus);
            var miembrosDto = miembros.Select(MapToDto);
            return Ok(miembrosDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener miembros por estatus {Estatus}", estatus);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<MiembroDto>> Create([FromBody] CreateMiembroDto createDto)
    {
        try
        {
            var miembro = MapFromCreateDto(createDto);
            var createdMiembro = await _miembroService.CreateMiembroAsync(miembro);
            var miembroDto = MapToDto(createdMiembro);
            return CreatedAtAction(nameof(GetById), new { id = miembroDto.Id }, miembroDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear el miembro");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<MiembroDto>> Update(int id, [FromBody] UpdateMiembroDto updateDto)
    {
        try
        {
            if (id != updateDto.Id)
            {
                return BadRequest("El Id del miembro no coincide");
            }

            var miembro = MapFromUpdateDto(updateDto);
            var updatedMiembro = await _miembroService.UpdateMiembroAsync(miembro);
            var miembroDto = MapToDto(updatedMiembro);
            return Ok(miembroDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el miembro con Id {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var result = await _miembroService.DeleteMiembroAsync(id);
            if (!result)
            {
                return NotFound($"Miembro con Id {id} no encontrado");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el miembro con Id {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    private static MiembroDto MapToDto(Miembro miembro)
    {
        return new MiembroDto
        {
            Id = miembro.Id,
            Nombre = miembro.Nombre,
            Apellido = miembro.Apellido,
            Celular = miembro.Celular,
            CorreoElectronico = miembro.CorreoElectronico,
            FechaIngreso = miembro.FechaIngreso,
            Direccion = miembro.Direccion,
            Member = miembro.Member,
            Cargo = miembro.Cargo,
            Rango = miembro.Rango,
            Estatus = miembro.Estatus,
            FechaNacimiento = miembro.FechaNacimiento,
            Cedula = miembro.Cedula,
            RH = miembro.RH,
            EPS = miembro.EPS,
            Padrino = miembro.Padrino,
            Foto = miembro.Foto,
            ContactoEmergencia = miembro.ContactoEmergencia,
            Ciudad = miembro.Ciudad,
            Moto = miembro.Moto,
            AnoModelo = miembro.AnoModelo,
            Marca = miembro.Marca,
            CilindrajeCC = miembro.CilindrajeCC,
            PlacaMatricula = miembro.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = miembro.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = miembro.FechaExpedicionSOAT
        };
    }

    private static Miembro MapFromCreateDto(CreateMiembroDto dto)
    {
        return new Miembro
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Celular = dto.Celular,
            CorreoElectronico = dto.CorreoElectronico,
            FechaIngreso = dto.FechaIngreso,
            Direccion = dto.Direccion,
            Member = dto.Member,
            Cargo = dto.Cargo,
            Rango = dto.Rango,
            Estatus = dto.Estatus,
            FechaNacimiento = dto.FechaNacimiento,
            Cedula = dto.Cedula,
            RH = dto.RH,
            EPS = dto.EPS,
            Padrino = dto.Padrino,
            Foto = dto.Foto,
            ContactoEmergencia = dto.ContactoEmergencia,
            Ciudad = dto.Ciudad,
            Moto = dto.Moto,
            AnoModelo = dto.AnoModelo,
            Marca = dto.Marca,
            CilindrajeCC = dto.CilindrajeCC,
            PlacaMatricula = dto.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dto.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dto.FechaExpedicionSOAT
        };
    }

    private static Miembro MapFromUpdateDto(UpdateMiembroDto dto)
    {
        return new Miembro
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Celular = dto.Celular,
            CorreoElectronico = dto.CorreoElectronico,
            FechaIngreso = dto.FechaIngreso,
            Direccion = dto.Direccion,
            Member = dto.Member,
            Cargo = dto.Cargo,
            Rango = dto.Rango,
            Estatus = dto.Estatus,
            FechaNacimiento = dto.FechaNacimiento,
            Cedula = dto.Cedula,
            RH = dto.RH,
            EPS = dto.EPS,
            Padrino = dto.Padrino,
            Foto = dto.Foto,
            ContactoEmergencia = dto.ContactoEmergencia,
            Ciudad = dto.Ciudad,
            Moto = dto.Moto,
            AnoModelo = dto.AnoModelo,
            Marca = dto.Marca,
            CilindrajeCC = dto.CilindrajeCC,
            PlacaMatricula = dto.PlacaMatricula,
            FechaExpedicionLicenciaConduccion = dto.FechaExpedicionLicenciaConduccion,
            FechaExpedicionSOAT = dto.FechaExpedicionSOAT
        };
    }
}
