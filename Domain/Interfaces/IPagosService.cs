using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Clase interface que define las operaciones que se pueden realizar sobre pagos, en el Microservicio Pagos.
    /// </summary>
    public interface IPagosService
    {
        /// <summary>
        /// Método que se encarga de obtener los pagos recibidos por un subastador en el Microservicio Pagos.
        /// </summary>
        /// <param name="correo">Parametro que corresponde al correo del subastador a consultar</param>
        /// <returns>Retorna una lista de objetos ReportePagosRecibidosSubastador con su detalle</returns>
        Task<List<ReportePagosRecibidosSubastador>> ObtenerPagosRecibidos(string correo);
    }
}
