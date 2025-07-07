using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.Query
{
    /// <summary>
    /// Clase Query que se encarga de enviar la solicitud para consultar el reporte de pujas realizads por un usuario y sus subastas.
    /// </summary>
    public class ReportePujasRealizadasUsuarioQuery : IRequest<List<ReportePujasUsuarioDTO>>
    {
        /// <summary>
        /// Atributo que contiene el correo del usuario quien consulta el reporte
        /// </summary>
        public string correoUsuario { get; set; }

        public ReportePujasRealizadasUsuarioQuery(string correoUsuario) 
        {
            this.correoUsuario = correoUsuario;
        }
    }
}
