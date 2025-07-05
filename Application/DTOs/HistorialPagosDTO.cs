using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace Application.DTOs
{
    public class HistorialPagosDTO
    {
        public Guid IdSubasta { get; set; }
        public string NombreSubasta { get; set; }
        public string DescripcionSubasta { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal IncrementoMinimo { get; set; }
        public decimal PrecioReserva { get; set; }

        public Guid Id { get; set; }
        public decimal MontoPago { get; set; }

        public DateTime CreatedAt { get; set; }
        public string UltimosDigitosTarjeta { get; set; }

        public Guid IdUsuario { get; set; }
    }
}
