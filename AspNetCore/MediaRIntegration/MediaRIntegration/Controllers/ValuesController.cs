using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaRIntegration.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new AvailabilityRequest());

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            var longString = new string('S', 12000);
            await _mediator.Send(new PaymentCommand());
        }
    }
}