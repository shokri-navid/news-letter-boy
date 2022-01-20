using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model;
using NewsLetterBoy.Service;
using NewsLetterBoy.WebApi.Dto.Request;
using NewsLetterBoy.WebApi.Dto.Response;

namespace NewsLetterBoy.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionService service, ILogger<SubscriptionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase>> Subscribe([FromBody] SubscribeRequest request)
        {
            if (ModelState.IsValid)
            {
                await _service.SubscribeAsync(request.UserId, request.NewsLetterId, request.ExpireDate);
                var response = new ResponseBase();
                return Ok(response);

            }

            return BadRequest(new ResponseBase<string>(
                ModelState.Values.SelectMany(x =>
                    x.Errors.Select(e => e.ErrorMessage)).ToArray()));
        }

        [HttpDelete("{userId}/newsletter/{newsLetterId}")]
        public async Task<ActionResult<ResponseBase>> Unsubscribe([FromRoute] int userId, [FromRoute] int newsLetterId)
        {

            await _service.UnsubscribeAsync(userId, newsLetterId);
            var response = new ResponseBase();
            return Ok(response);
        }

        [HttpGet("{userId}/newsletter/{newsLetterId}")]
        public async Task<ActionResult<ResponseBase<bool>>> CheckSubscription([FromRoute] int userId,
            [FromQuery] int newsLetterId)
        {

            var status = await _service.GetSubscriptionStatusAsync(userId, newsLetterId);
            var response = new ResponseBase<bool>(status);
            return Ok(response);

        }
    }
}