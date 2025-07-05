using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Object;
using Domain.Value_Objects;

namespace Domain.Entities
{
    public class ReportePagosRecibidosSubastador
    {
        public Guid IdSubasta { get; set; }
        public NombreSubastaVO NombreSubasta { get; set; }
        public DescripcionSubastaVO DescripcionSubasta { get; set; }
        public FechaInicioSubastaVO FechaInicioSubasta { get; set; }
        public FechaFinSubastaVO FechaFinSubasta { get; set; }
        public IncrementoMinimoSubastaVO IncrementoMinimoSubasta { get; set; }
        public PrecioReservaSubastaVO PrecioReservaSubasta { get; set; }
        public EstadoSubastaVO EstadoSubasta { get; set; }

        public Guid IdPago { get; set; }
        public MontoHistorialPagosVO MontoPago { get; set; }

        public FechaPagoVO FechaPago { get; set; }

        public UltimosCuatroDigitosTarjetaPagoVO UltimosCuatroDigitosTarjetaPago { get; set; }

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
