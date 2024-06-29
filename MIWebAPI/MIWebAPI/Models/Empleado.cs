namespace MIWebAPI.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }

        public string? NombreDescripcion { get; set; }

        public string? Identificacion { get; set; }

        public string? Moneda { get; set; }

        public string? FechaCreacion { get; set; }
        public object? Direccion { get; internal set; }
    }
}
