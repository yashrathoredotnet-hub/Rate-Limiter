using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rate_Limiter.Interfaces;
using Rate_Limiter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rate_Limiter.Controller
{
    [ApiController]
    [Route("check")]
    public sealed class RateLimitController : ControllerBase
    {
        private readonly IRateLimiter _rateLimiter;

        public RateLimitController(IRateLimiter rateLimiter)
        {
            _rateLimiter = rateLimiter;
        }

        [HttpPost]
        public IActionResult Check([FromBody] RateLimitRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Identifier))
                return BadRequest("Identifier is required.");

            return _rateLimiter.IsAllowed(request.Identifier)
                ? Ok()
                : StatusCode(StatusCodes.Status429TooManyRequests);
        }
    }
}
