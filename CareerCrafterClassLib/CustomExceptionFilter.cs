using System;

namespace CareerCrafterClassLib.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string message) : base(message) { }
    }
}
