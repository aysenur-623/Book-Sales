﻿namespace BS.Business.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
           : base(message)
        {

        }
    }
}
