using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exception;
using Application.Exceptions;
using Application.Query;
using Application.Services;
using Domain.Interfaces;
using MediatR;

namespace Application.Handler
{
    /// <summary>
    /// Clase Handler que se encarga de realizar la consulta del reporte de las pujas realizadas por un usuario en cada subasta.
    /// </summary>
    public class ReportePujasRealizadasUsuarioHandler : IRequestHandler<ReportePujasRealizadasUsuarioQuery, List<ReportePujasUsuarioDTO>>
    {
        /// <summary>
        /// Atributo que corresponde a las operaciones posibles que se pueden realizar sobre una puja en el Microservicio Pujas, el cual será inyectado por inversión de dependencias.
        /// </summary>
        private readonly IPujaService _pujaService;
        /// <summary>
        /// Atributo que corresponde a las operaciones posibles que se pueden realizar sobre un usuario en el Microservicio Usuarios, el cual será inyectado por inversión de dependencias.
        /// </summary>
        private readonly IUsuarioService _usuarioService;

        public ReportePujasRealizadasUsuarioHandler(IPujaService pujaService, IUsuarioService usuarioService)
        {
            _pujaService = pujaService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Metodo que se encarga de consultar las puajs realizadas por un usuario, agrupadas por subastas.
        /// </summary>
        /// <param name="request">Parametro que contiene el correo del usuario.</param>
        /// <returns>Retorna un lista de DTOs con los datos de las subastas y pujas realizadas del usuario.</returns>
        /// <exception cref="UsuarioNoEncontradoException">
        /// Esta excepcion ocurre si no se pudo obtener el ID del usuario en el Microservicio Usuarios.
        /// </exception>
        /// <exception cref="FalloAlObtenerPujaException">
        /// Esta excepcion ocurre si no se pudo obtener las pujas desde el Microservicio Pujas o si ocurre un error inesperado.
        /// </exception>
        public async Task<List<ReportePujasUsuarioDTO>> Handle(ReportePujasRealizadasUsuarioQuery request, CancellationToken cancellationToken)
        {
          try
          {
            //Se obtiene el ID del usuario desde el Microservicio Usuarios
            var idUsuario = await _usuarioService.ObtenerUsuarioPorIdAsync(request.correoUsuario);
            
            //En caso de que la consulta no retorne algún valor, se lanza la excepción
            if (idUsuario==Guid.Empty)
               throw new UsuarioNoEncontradoException();

            //Se obtienen las pujas realizadas por un usuario en cada subasta desde el Microservicio Pujas
            var reportePujasUsuario = await _pujaService.ObtenerReportePujasPorUsuarioAsync(request.correoUsuario);

            var dtos = reportePujasUsuario.Select(e => new ReportePujasUsuarioDTO
            {
                IdSubasta = e.IdSubasta,
                NombreSubasta = e.NombreSubasta.Nombre,
                Pujas = e.ListaPujas.Select(p => new PujaDTO
                {
                    id = p.Id,
                    montoPuja = p.MontoPuja.montoPuja,
                    montoMaximo = p.MontoMaximo.montoMaximo,
                    tipoPuja = p.TipoPuja.tipoPuja,
                    montoPredeterminado = p.MontoPredeterminado.montoPredeterminado,
                    fecha = p.FechaPuja.fechaPuja
                }).ToList()
            }).ToList();

            return dtos;
          }
          catch (UsuarioNoEncontradoException)
          {
               throw;
          }
          catch (System.Exception ex)
          {
               throw new FalloAlObtenerPujaException("Error al generar el reporte de pujas", ex);
          }
        }

    }
}
