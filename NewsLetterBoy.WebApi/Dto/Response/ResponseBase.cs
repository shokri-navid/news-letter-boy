using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NewsLetterBoy.WebApi.Dto.Response
{
    public class ResponseBase <T> : ResponseBase
    {
       

        public ResponseBase(T data)
        {
            Data = data;
            Message = new[] {"Successful Operation"};
        }
        
        public ResponseBase(ModelStateDictionary modelState)
        {
            Data = default(T);
            Message = modelState.SelectMany(x => 
                x.Value.Errors.Select(e => e.ErrorMessage)).ToArray(); 
        }
        
        public ResponseBase(string[] messages)
        {
            Data = default(T);
            Message = messages;
        }
        public T Data { get; private set; }
        

        
    }

    public class ResponseBase
    {
        public ResponseBase() {
        }

        public ResponseBase(ModelStateDictionary modelState)
        {
            Message = modelState.SelectMany(x => 
                x.Value.Errors.Select(e => e.ErrorMessage)).ToArray(); 
        }
        
        public ResponseBase(string[] messages)
        {
            Message = messages;
        }
        public string[] Message { get; protected set; }

    }
}