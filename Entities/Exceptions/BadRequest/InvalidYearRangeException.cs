namespace Entities.Exceptions.BadRequest
{
    public sealed class InvalidYearRangeException : BadRequestException
    {
        public InvalidYearRangeException(int min, int max)
            : base($"Max age {max} has to be more or equal to Min age {min}")
        {
        }
    }

}