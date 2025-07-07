using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Application.Exceptions;
using Application.Query;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handler
{
    /// <summary>
    /// Clase Handler que se encarga de realizar la consulta del reporte de las subastas realizadas y sus pujas.
    /// </summary>
    public class ReporteSubastasRealizadasHandler : IRequestHandler<ReporteSubastasRealizadasQuery, List<ReporteSubastasDTO>>
    {
        /// <summary>
        /// Atributo que corresponde a las operaciones posibles que se pueden realizar sobre una subasta en el Microservicio Subastas, el cual será inyectado por inversión de dependencias.
        /// </summary>
        private readonly ISubastaService _subastaService;
        /// <summary>
        /// Atributo que corresponde a las operaciones posibles que se pueden realizar sobre un usuario en el Microservicio Usuarios, el cual será inyectado por inversión de dependencias.
        /// </summary>
        private readonly IUsuarioService _usuarioService;

        public ReporteSubastasRealizadasHandler(ISubastaService subastaService, IUsuarioService usuarioService)
        {
            _subastaService = subastaService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Metodo que se encarga de consultar las subastas finalizadas y sus pujas.
        /// </summary>
        /// <returns>Retorna un lista de DTOs con los datos de las subastas y sus pujas.</returns>
        /// <exception cref="UsuarioNoEncontradoException">
        /// Esta excepcion ocurre si no se pudo obtener el ID del usuario en el Microservicio Usuarios.
        /// </exception>
        /// <exception cref="FalloAlObtenerSubastasException">
        /// Esta excepcion ocurre si no se pudo obtener los pagos desde el Microservicio Pagos o si ocurre un error inesperado.
        /// </exception>
        public async Task<List<ReporteSubastasDTO>> Handle(ReporteSubastasRealizadasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Se obtienen las subastas finalizadas desde el Microservicio Subastas
                var subastas = await _subastaService.ObtenerSubastas();

                var listaSubastas = subastas.Select(async e => new ReporteSubastasDTO
                {
                    IdSubasta = e.IdSubasta,
                    correoUsuario = await _usuarioService.ObtenerCorreoPorIdAsync(e.IdUsuario) ?? throw new UsuarioNoEncontradoException(),
                    NombreSubasta = e.NombreSubasta.Nombre,
                    DescripcionSubasta = e.DescripcionSubasta.descripcion,
                    Estado = e.EstadoSubasta.estado,
                    FechaInicio = e.FechaInicioSubasta.fechaInicio,
                    FechaFin = e.FechaFinSubasta.fechaFin,
                    incrementoMinimo = e.IncrementoMinimoSubasta.incrementoMinimo,
                    precioReserva = e.PrecioReservaSubasta.precioReserva,
                    IdProducto = e.IdProducto,
                    NombreProducto = e.NombreProducto.Nombre,
                    DescripcionProducto = e.DescripcionProducto.descripcion,
                    PrecioBase = e.PrecioBaseProducto.precio,
                    Categoria = e.CategoriaProducto.categoria,
                    urlImagen = e.ImagenURLProducto.url,

                    Pujas = e.ListaPujas.Select(p => new PujaDTO
                    {
                        id = p.Id,
                        montoPuja = p.MontoPuja.montoPuja,
                        correoUsuario = p.CorreoUsuarioPuja,
                        montoMaximo = p.MontoMaximo.montoMaximo,
                        tipoPuja = p.TipoPuja.tipoPuja,
                        montoPredeterminado = p.MontoPredeterminado.montoPredeterminado,
                        fecha = p.FechaPuja.fechaPuja
                    }).ToList()
                });

                var dtos = await Task.WhenAll(listaSubastas);
                return dtos.ToList();

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

