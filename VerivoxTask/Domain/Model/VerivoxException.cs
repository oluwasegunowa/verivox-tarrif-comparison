using System;

namespace VerivoxTask.Domain.Model
{
    public class VerivoxException : Exception
    {
        public VerivoxException(string message) : base(message)
        {
        }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
