using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CertificationController(IMediator mediator) => _mediator = mediator;


        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCertifications()
        {
            var getAvailableCertifications = new GetAvailableCertificationsQuery();

            var response = await _mediator.Send(getAvailableCertifications);
            return Ok(response);
        }
    }
}