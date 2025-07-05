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
        [HttpPost("obtenerReporteSubastas")]
        public async Task<IActionResult> ObtenerReporteSubastas()
        {
            var resultado = await _mediator.Send(new ReporteSubastasRealizadasQuery());
            return Ok(resultado);
        }
    }
}
