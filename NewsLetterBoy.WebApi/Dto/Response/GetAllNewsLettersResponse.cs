using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NewsLetterBoy.WebApi.Dto.Response
{
    public class GetAllNewsLettersResponse : ResponseBase<IEnumerable<NewsLetterDto>>
    {
        public GetAllNewsLettersResponse(IEnumerable<NewsLetterDto> data) : base(data)
        {
        }

        public GetAllNewsLettersResponse(ModelStateDictionary modelState) : base(modelState)
        {
        }

        public GetAllNewsLettersResponse(string[] messages) : base(messages)
        {
        }
    }
}