using System;
using System.ComponentModel.DataAnnotations;

namespace NewsLetterBoy.WebApi.Dto.Request
{
    public class CreateSubscriptionRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int NewsLetterId { get; set; }
        public DateTime? ExpireDatetime { get; set; }
    }
}