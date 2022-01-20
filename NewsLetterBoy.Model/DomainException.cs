using System;

namespace NewsLetterBoy.Model
{
    public class DomainException : Exception
    {
        public int ErrorCode { get; private set; }

        public DomainException(string message, int code) : base(message)
        {
            ErrorCode = code;
        }
    }
}