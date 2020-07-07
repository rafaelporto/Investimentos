using System;

namespace Investimentos.Application.Exceptions
{
    public class InvestimentosApplicationException : Exception
    {
        internal InvestimentosApplicationException(string message) : base(message) { }
        internal InvestimentosApplicationException(string message, Exception inner) : base(message, inner) { }
    }
}