using System.ComponentModel.DataAnnotations;
public class UsuarioEmpleadoModel{
    [Required(ErrorMessage = "El campo Login es obligatorio")]
    public string? Login{get; set;}
    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    public string? Nombre{get; set;}
    [Required(ErrorMessage = "El campo Paterno es obligatorio")]
    public string? Paterno{get; set;}
    [Required(ErrorMessage = "El campo Materno es obligatorio")]
    public string? Materno{get; set;}
    [Required(ErrorMessage = "El campo Sueldo es obligatorio")]
    public double? Sueldo{get; set;}
    [Required(ErrorMessage = "El campo Fecha Ingreso es obligatorio")]
    public string? FechaIngreso{get; set;}
}