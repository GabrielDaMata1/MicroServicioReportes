using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    /// <summary>
    /// Clase Exception que se encarga de manejar los errores producidos al obtener una subasta en el Microservicio Subastas.
    /// </summary>
    public class FalloAlObtenerSubastasException: System.Exception
    {
        public FalloAlObtenerSubastasException() : base("Ha ocurrido un error al obtener las subasta.") { }

        public FalloAlObtenerSubastasException(string mensaje) : base(mensaje) { }

        public FalloAlObtenerSubastasException(string mensaje, System.Exception innerException)
            : base(mensaje, innerException) { }
    }
}
