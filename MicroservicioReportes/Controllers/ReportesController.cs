using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioReportes.Controllers
{
    [ApiController]
    [Route("api/Reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet("obtenerReporteSubastas")]
        public async Task<IActionResult> ObtenerReporteSubastas()
        {
            var resultado = await _mediator.Send(new ReporteSubastasRealizadasQuery());
            return Ok(resultado);
        }

        [HttpGet("obtenerReportePujasRealizadasUsuario/{correo}")]
        public async Task<IActionResult> ObtenerReportePujasRealizadasUsuario(string correo)
        {
            var resultado = await _mediator.Send(new ReportePujasRealizadasUsuarioQuery(correo));
            return Ok(resultado);
        }

        [HttpGet("obtenerPagosRecibidosSubastador/{correo}")]
        public async Task<IActionResult> ObtenerPagosRecibidosSubastador(string correo)
        {
            var resultado = await _mediator.Send(new ReportePagosRecibidosSubastadorQuery(correo));
            return Ok(resultado);
        }
    }
}
