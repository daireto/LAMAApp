namespace LAMAApp.Application.DTOs;

public class UpdateMiembroDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string Celular { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string? CorreoElectronico { get; set; }
    public string? Direccion { get; set; }
    public string? RH { get; set; }
    public string? EPS { get; set; }
    public string? Foto { get; set; }
    public string? ContactoEmergencia { get; set; }
    public int Member { get; set; }
    public DateTime FechaIngreso { get; set; }
    public string Rango { get; set; } = string.Empty;
    public string Estatus { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public string? Padrino { get; set; }
    public string Moto { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string PlacaMatricula { get; set; } = string.Empty;
    public int? AnoModelo { get; set; }
    public int? CilindrajeCC { get; set; }
    public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
    public DateTime? FechaExpedicionSOAT { get; set; }
}
