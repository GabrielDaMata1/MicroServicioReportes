using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Clase interface que define las operaciones que se pueden realizar sobre subastas, en el Microservicio Subasta.
    /// </summary>
    public interface ISubastaService
    {
        /// <summary>
        /// Método que se encarga de obtener las subastas con sus pujas en el Microservicio Subastas.
        /// </summary>
        /// <returns>Retorna una lista de objetos SubastaReporte con su detalle</returns>
        Task<List<SubastaReporte>> ObtenerSubastas();    
    }
}
