using System;

namespace NewsLetterBoy.WebApi.Dto.Request
{
    public class SubscribeRequest
    {
        public int UserId { get; set; }
        public int NewsLetterId { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}