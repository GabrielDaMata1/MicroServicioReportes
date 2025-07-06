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
    public class ReporteSubastasRealizadasHandler : IRequestHandler<ReporteSubastasRealizadasQuery, List<ReporteSubastasDTO>>
    {
        private readonly ISubastaService _subastaService;
        private readonly IUsuarioService _usuarioService;

        public ReporteSubastasRealizadasHandler(ISubastaService subastaService, IUsuarioService usuarioService)
        {
            _subastaService = subastaService;
            _usuarioService = usuarioService;
        }
        public async Task<List<ReporteSubastasDTO>> Handle(ReporteSubastasRealizadasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subastas = await _subastaService.ObtenerSubastas();

                var listaSubastas = subastas.Select(async e => new ReporteSubastasDTO
                {
                    IdSubasta = e.IdSubasta,
                    correoUsuario = await _usuarioService.ObtenerCorreoPorIdAsync(e.IdUsuario),
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

