using System;

namespace Investimentos.Application.Exceptions
{
    [System.Serializable]
    public class AdapterException : Exception
    {
        public AdapterException(string message) : base(message) { }
        public AdapterException(string message, System.Exception inner) : base(message, inner) { }
    }
}