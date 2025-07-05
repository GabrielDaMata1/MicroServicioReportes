using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Application.Exceptions;
using Application.Query;
using Domain.Interfaces;
using MediatR;

namespace Application.Handler
{
    public class ReportePagosRecibidosSubastadorHandler : IRequestHandler<ReportePagosRecibidosSubastadorQuery, List<ReportePagosRecibidosSubastadorDTO>>
    {
        private readonly IPagosService _pagosService;
        private readonly IUsuarioService _usuarioService;

        public ReportePagosRecibidosSubastadorHandler(IPagosService pagosService, IUsuarioService usuarioService)
        {
            _pagosService = pagosService;
            _usuarioService = usuarioService;
        }
        public async Task<List<ReportePagosRecibidosSubastadorDTO>> Handle(ReportePagosRecibidosSubastadorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pagos = await _pagosService.ObtenerPagosRecibidos(request.correoUsuario);

                var resultado = new List<ReportePagosRecibidosSubastadorDTO>();

                foreach (var pago in pagos)
                {
                    var correoUsuario = await _usuarioService.ObtenerCorreoPorIdAsync(pago.IdUsuario) ?? throw new UsuarioNoEncontradoException();
                    var dto = new ReportePagosRecibidosSubastadorDTO
                    {
                        IdSubasta = pago.IdSubasta,
                        NombreSubasta = pago.NombreSubasta.Nombre,
                        DescripcionSubasta = pago.DescripcionSubasta.descripcion,
                        Estado = pago.EstadoSubasta.estado,
                        FechaInicio = pago.FechaInicioSubasta.fechaInicio,
                        FechaFin = pago.FechaFinSubasta.fechaFin,
                        IncrementoMinimo = pago.IncrementoMinimoSubasta.incrementoMinimo,
                        PrecioReserva = pago.PrecioReservaSubasta.precioReserva,
                        correo = correoUsuario,
                        Id = pago.IdPago,
                        MontoPago = pago.MontoPago.montoPago,
                        CreatedAt = pago.FechaPago.fechaPago,
                        UltimosDigitosTarjeta = pago.UltimosCuatroDigitosTarjetaPago.ultimosCuatroDigitosTarjetaPago,
                    };

                    resultado.Add(dto);
                }

                return resultado;
            }
            catch (UsuarioNoEncontradoException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new FalloAlObtenerSubastasException("Error al generar el reporte de subastas", ex);
            }
        }
    }
}
