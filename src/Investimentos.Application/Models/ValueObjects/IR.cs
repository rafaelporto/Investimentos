using System;

namespace Investimentos.Application.Models.ValueObjects
{
    public struct IR
    {
        private readonly decimal _valorTotal;
        private readonly decimal _valorInvestido;
        private readonly decimal _taxaRentabilidade;
        public decimal Value { get; private set; }

        public IR(decimal valorTotal, decimal valorInvestido, decimal taxaRentabilidade)
        {
            _valorTotal = valorTotal;
            _valorInvestido = valorInvestido;
            _taxaRentabilidade = taxaRentabilidade;
            Value = (_valorTotal - _valorInvestido) * _taxaRentabilidade;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator decimal(IR input) => input.Value;
    }
}