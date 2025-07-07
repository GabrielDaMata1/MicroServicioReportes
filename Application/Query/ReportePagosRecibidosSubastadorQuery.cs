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
    /// Clase Query que se encarga de enviar la solicitud para consultar el reporte de pagos recibidos por un subastador.
    /// </summary>
    public class ReportePagosRecibidosSubastadorQuery : IRequest<List<ReportePagosRecibidosSubastadorDTO>>
    {
        /// <summary>
        /// Atributo que contiene el correo del subastador quien consulta el reporte
        /// </summary>
        public string correoUsuario { get; set; }

        public ReportePagosRecibidosSubastadorQuery(string correoUsuario)
        {
            this.correoUsuario = correoUsuario;
        }
    }
}
