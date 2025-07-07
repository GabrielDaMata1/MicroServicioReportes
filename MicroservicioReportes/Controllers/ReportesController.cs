using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioReportes.Controllers
{
    /// <summary>
    /// Clase controller API encargada de procesar las solicitudes de consulta sobre los reportes de pujas, subastas y pagos.
    /// </summary>
    [ApiController]
    [Route("api/Reportes")]
    public class ReportesController : ControllerBase
    {
        /// <summary>
        /// Atributo que se encarga de enviar solicitudes (commands/queries) mediante el patrón mediador
        /// </summary>
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;

        }

        /// <summary>
        /// Endpoint encargado de obtener el reporte de las subastas finalizadas con sus pujas.
        /// </summary>
        /// <returns>Retorna una lista de DTOs con el detalle de cada subasta y sus pujas.</returns>
        [HttpGet("obtenerReporteSubastas")]
        public async Task<IActionResult> ObtenerReporteSubastas()
        {
            var resultado = await _mediator.Send(new ReporteSubastasRealizadasQuery());
            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint encargado de obtener el reporte de las pujas de un usuario en todas las subastas.
        /// </summary>
        /// <param name="correo">Parametro que corresponde al correo del usuario.</param>
        /// <returns>Retorna una lista de DTOs con los detalles de la puja y el detalle de la subasta.</returns>

        [HttpGet("obtenerReportePujasRealizadasUsuario/{correo}")]
        public async Task<IActionResult> ObtenerReportePujasRealizadasUsuario(string correo)
        {
            var resultado = await _mediator.Send(new ReportePujasRealizadasUsuarioQuery(correo));
            return Ok(resultado);
        }

        /// <summary>
        /// Endpoint encargado de obtener el reporte de los pagos recibidos por un subastador.
        /// </summary>
        /// <param name="correo">Parametro de que corresponde al correo del subastador cuyos pagos se van a consultar.</param>
        /// <returns>Retorna una lista de DTOs con los detalles del pago y de la subasta que fue pagada.</returns>

        [HttpGet("obtenerPagosRecibidosSubastador/{correo}")]
        public async Task<IActionResult> ObtenerPagosRecibidosSubastador(string correo)
        {
            var resultado = await _mediator.Send(new ReportePagosRecibidosSubastadorQuery(correo));
            return Ok(resultado);
        }
    }
}
