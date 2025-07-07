using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace Application.DTOs
{
    /// <summary>
    /// Clase DTO que se encarga de mostrar la información de un pago de una subasta desde el Microservicio Pagos.
    /// </summary>
    public class HistorialPagosDTO
    {
        /// <summary>
        /// Atributo que corresponde al ID de la subasta.
        /// </summary>
        public Guid IdSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al nombre de la subasta.
        /// </summary>
        public string NombreSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la descripcion de la subasta.
        /// </summary>
        public string DescripcionSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al estado de la subasta.
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha de inicio de la subasta.
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha fin de la subasta.
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Atributo que corresponde al incremento minimo de la subasta.
        /// </summary>
        public decimal IncrementoMinimo { get; set; }
        /// <summary>
        /// Atributo que corresponde al precio de reserva de la subasta.
        /// </summary>
        public decimal PrecioReserva { get; set; }
        /// <summary>
        /// Atributo que corresponde al ID del pago.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Atributo que corresponde al monto del pago.
        /// </summary>
        public decimal MontoPago { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha del pago.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Atributo que corresponde a los ultimos cuatro digitos de la tarjeta con la que se hizo el pago.
        /// </summary>
        public string UltimosDigitosTarjeta { get; set; }
        /// <summary>
        /// Atributo que corresponde al ID del usuario que hizo el pago.
        /// </summary>
        public Guid IdUsuario { get; set; }
    }
}
