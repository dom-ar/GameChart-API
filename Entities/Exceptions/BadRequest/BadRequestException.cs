﻿namespace Entities.Exceptions.BadRequest
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message)
            : base(message)
        {
        }
    }

}
