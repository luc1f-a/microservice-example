using System.Diagnostics.CodeAnalysis;

namespace TBot.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}