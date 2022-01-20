using System.ComponentModel.DataAnnotations;

namespace NewsLetterBoy.WebApi.Dto.Request
{
    public class UnsubscribeRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int NewsLetterId { get; set; }
    }
}