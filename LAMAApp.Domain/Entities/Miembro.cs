using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAMAApp.Domain.Entities;

[Table("Miembros")]
public class Miembro
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Cedula { get; set; } = string.Empty;
    
    [Required]
    public DateTime FechaNacimiento { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Celular { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Ciudad { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? CorreoElectronico { get; set; }
    
    [MaxLength(255)]
    public string? Direccion { get; set; }
    
    [MaxLength(5)]
    public string? RH { get; set; }
    
    [MaxLength(100)]
    public string? EPS { get; set; }
    
    [MaxLength(255)]
    public string? Foto { get; set; }
    
    [MaxLength(255)]
    public string? ContactoEmergencia { get; set; }
    
    [Required]
    public int Member { get; set; }
    
    [Required]
    public DateTime FechaIngreso { get; set; }

    [Required]
    [MaxLength(100)]
    public string Rango { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Estatus { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Cargo { get; set; }
    
    [MaxLength(100)]
    public string? Padrino { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Moto { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Marca { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string PlacaMatricula { get; set; } = string.Empty;
    
    public int? AnoModelo { get; set; }
    
    public int? CilindrajeCC { get; set; }
    
    public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
    
    public DateTime? FechaExpedicionSOAT { get; set; }
}
