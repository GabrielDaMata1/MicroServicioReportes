using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Clase interface que define las operaciones que se pueden realizar sobre pujas, en el Microservicio Pujas.
    /// </summary>
    public interface IPujaService
    {
        /// <summary>
        /// Método que se encarga de obtener las pujas agrupadas por subastas realziads por un usuario en el Microservicio Pujas.
        /// </summary>
        /// <param name="correo">Parametro que corresponde al correo del usuario a consultar</param>
        /// <returns>Retorna una lista de objetos ReportePujaUsuario con su detalle</returns>
        Task<List<ReportePujasUsuario>> ObtenerReportePujasPorUsuarioAsync(string correo);
    }
}
