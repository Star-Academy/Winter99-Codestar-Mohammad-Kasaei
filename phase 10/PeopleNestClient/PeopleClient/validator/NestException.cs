using System;

namespace PeopleClientLibrary.validator
{
    public class NestException : Exception
    {
        public NestException()
            : base("General Nest exception occured")
        {
        }

        public NestException(string message)
            : base(message)
        {
        }
    }
}