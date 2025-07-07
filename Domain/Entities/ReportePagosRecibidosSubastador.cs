using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Object;
using Domain.Value_Objects;

namespace Domain.Entities
{
    /// <summary>
    /// Clase Entity que representa a la entidad ReportePagosRecibidosSubastador en el dominio del sistema.
    /// </summary>
    public class ReportePagosRecibidosSubastador
    {
        /// <summary>
        /// Atributo que corresponde al ID de la subasta.
        /// </summary>
        public Guid IdSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al nombre de la subasta.
        /// </summary>
        public NombreSubastaVO NombreSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la descripcion de la subasta.
        /// </summary>
        public DescripcionSubastaVO DescripcionSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha de inicio de la subasta.
        /// </summary>
        public FechaInicioSubastaVO FechaInicioSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde a la fecha fin de la subasta.
        /// </summary>
        public FechaFinSubastaVO FechaFinSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al incremento minimo de la subasta.
        /// </summary>
        public IncrementoMinimoSubastaVO IncrementoMinimoSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al precio de reserva de la subasta.
        /// </summary>
        public PrecioReservaSubastaVO PrecioReservaSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al estado de la subasta.
        /// </summary>
        public EstadoSubastaVO EstadoSubasta { get; set; }
        /// <summary>
        /// Atributo que corresponde al ID del pago de la subasta.
        /// </summary>
        public Guid IdPago { get; set; }
        /// <summary>
        /// Atributo que corresponde al monto del pago de la subasta.
        /// </summary>
        public MontoHistorialPagosVO MontoPago { get; set; }
        /// <summary>
        /// Atributo que corresponde la fecha del pago de la subasta.
        /// </summary>
        public FechaPagoVO FechaPago { get; set; }
        /// <summary>
        /// Atributo que corresponde a los ultimos cuatro digitos del medio con el que se realizó el pago de la subasta.
        /// </summary>
        public UltimosCuatroDigitosTarjetaPagoVO UltimosCuatroDigitosTarjetaPago { get; set; }
        /// <summary>
        /// Atributo que corresponde al ID del usuario que realizó el pago de la subasta.
        /// </summary>
        public Guid IdUsuario { get; set; }

        public ReportePagosRecibidosSubastador(Guid idSubasta, NombreSubastaVO nombreSubasta, DescripcionSubastaVO descripcionSubasta, FechaInicioSubastaVO fechaInicioSubasta,
            FechaFinSubastaVO fechaFinSubasta, IncrementoMinimoSubastaVO incrementoMinimoSubasta, PrecioReservaSubastaVO precioReservaSubasta, EstadoSubastaVO estadoSubasta, Guid idPago,
            MontoHistorialPagosVO montoPago, FechaPagoVO fechaPago, UltimosCuatroDigitosTarjetaPagoVO ultimosCuatroDigitosTarjetaPago, Guid idUsuario)
        {
            IdSubasta = idSubasta;
            NombreSubasta = nombreSubasta;
            DescripcionSubasta = descripcionSubasta;
            FechaInicioSubasta = fechaInicioSubasta;
            FechaFinSubasta = fechaFinSubasta;
            IncrementoMinimoSubasta = incrementoMinimoSubasta;
            PrecioReservaSubasta = precioReservaSubasta;
            EstadoSubasta = estadoSubasta;
            IdPago = idPago;
            MontoPago = montoPago;
            FechaPago = fechaPago;
            UltimosCuatroDigitosTarjetaPago = ultimosCuatroDigitosTarjetaPago;
            IdUsuario = idUsuario;
        }
    }
}
