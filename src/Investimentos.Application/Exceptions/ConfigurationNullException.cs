using System;

namespace Investimentos.Application.Exceptions
{
    public class ConfigurationNullException : Exception
    {
        internal ConfigurationNullException(string message) : base(message) { }
        internal ConfigurationNullException(string message, Exception inner) : base(message, inner) { }
    }
}