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
    public class NewsLetterController : ControllerBase
    {
        private readonly INewsLetterService _newsLetterService;
        private readonly ILogger<NewsLetterController> _logger;

        public NewsLetterController(INewsLetterService newsLetterService, ILogger<NewsLetterController> logger)
        {
            _newsLetterService = newsLetterService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseBase<int>>> Create([FromBody] CreateNewsLetterRequest request)
        {
            if (ModelState.IsValid)
            {

                var response =
                    new ResponseBase<int>(await _newsLetterService.CreateAsync(request.Title, request.Description));
                return Ok(response);


            }

            return BadRequest(new ResponseBase<string>(
                ModelState.Values.SelectMany(x=>
                    x.Errors.Select(e=>e.ErrorMessage)).ToArray()));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseBase>> Create([FromRoute] int id,
            [FromBody] EditNewsLetterRequest request)
        {
            if (ModelState.IsValid)
            {

                await _newsLetterService.UpdateAsync(id, request.Title, request.Description);
                return Ok(new ResponseBase());

            }

            return BadRequest(new ResponseBase(ModelState));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseBase<NewsLetterDto>>> Get([FromRoute] int id)
        {
            var newsLetter = await _newsLetterService.GetByIdAsync(id);
            if (newsLetter == null)
            {
                return NotFound(new ResponseBase<NewsLetterDto>(new[] {""}));
            }

            var dto = new NewsLetterDto
            {
                Description = newsLetter.Description,
                Id = newsLetter.Id,
                Title = newsLetter.Title
            };
            return Ok(new ResponseBase<NewsLetterDto>(dto));
        }

        [HttpGet]
        public async Task<ActionResult<GetAllNewsLettersResponse>> Create([FromQuery] string filter)
        {
            var newsLetters = (await _newsLetterService.GetAllAsync(filter)).ToList();
            if (!newsLetters.Any())
            {
                return NotFound(new GetAllNewsLettersResponse(new[] {""}));
            }

            var response = newsLetters.Select(x => new NewsLetterDto
            {
                Description = x.Description,
                Id = x.Id,
                Title = x.Title
            });
            return Ok(new GetAllNewsLettersResponse(response));
        }
    }
}