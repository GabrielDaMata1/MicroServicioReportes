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
    /// <summary>
    /// Clase Handler que se encarga de realizar la consulta del reporte de los pagos recibidos por un subastador.
    /// </summary>
    public class ReportePagosRecibidosSubastadorHandler : IRequestHandler<ReportePagosRecibidosSubastadorQuery, List<ReportePagosRecibidosSubastadorDTO>>
    {
        /// <summary>
        /// Atributo que corresponde a las operaciones posibles que se pueden realizar sobre los pagos en el Microservicio Pagos, el cual será inyectado por inversión de dependencias.
        /// </summary>
        private readonly IPagosService _pagosService;
        /// <summary>
        /// Atributo que corresponde a las operaciones posibles que se pueden realizar sobre un usuario en el Microservicio Usuarios, el cual será inyectado por inversión de dependencias.
        /// </summary>
        private readonly IUsuarioService _usuarioService;

        public ReportePagosRecibidosSubastadorHandler(IPagosService pagosService, IUsuarioService usuarioService)
        {
            _pagosService = pagosService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Metodo que se encarga de consultar los pagos recibidos por un subastador.
        /// </summary>
        /// <param name="request">Parametro que contiene el correo del subastador.</param>
        /// <returns>Retorna un lista de DTOs con los datos de los pagos recibidos.</returns>
        /// <exception cref="UsuarioNoEncontradoException">
        /// Esta excepcion ocurre si no se pudo obtener el ID del usuario en el Microservicio Usuarios.
        /// </exception>
        /// <exception cref="FalloAlObtenerSubastasException">
        /// Esta excepcion ocurre si no se pudo obtener los pagos desde el Microservicio Pagos o si ocurre un error inesperado.
        /// </exception>
        public async Task<List<ReportePagosRecibidosSubastadorDTO>> Handle(ReportePagosRecibidosSubastadorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Se obtienen los pagos recibidos por un subastador desde el Microservicio Pagos
                var pagos = await _pagosService.ObtenerPagosRecibidos(request.correoUsuario);

                var resultado = new List<ReportePagosRecibidosSubastadorDTO>();

                foreach (var pago in pagos)
                {
                    //Se obtiene el correo del usuario que realizó el pago desde el Microservicio Usuarios
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
                throw new FalloAlObtenerSubastasException("Error al generar el reporte de pagos", ex);
            }
        }
    }
}
