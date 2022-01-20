using System.ComponentModel.DataAnnotations;

namespace NewsLetterBoy.WebApi.Dto.Request
{
    public class EditNewsLetterRequest
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}