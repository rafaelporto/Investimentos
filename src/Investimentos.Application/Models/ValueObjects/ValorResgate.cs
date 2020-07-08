using System;

namespace Investimentos.Application.Models.ValueObjects
{
    public struct ValorResgate
    {
        private readonly DateTime _dataFim;
        private readonly DateTime _dataInicio;
        private readonly decimal _valorInicial;
        private decimal? _value;
        public decimal Value
        {
            get 
            {
                if (_value is null)
                    _value = CalculaValor();

                return _value.Value;
            }
        }

        public ValorResgate(DateTime dataInicio, DateTime dataFim, decimal valorInicio)
        {
            _dataInicio = dataInicio;
            _dataFim = dataFim;
            _valorInicial = valorInicio;
            _value = null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator decimal(ValorResgate input) => input.Value;

        private decimal CalculaValor()
        {
            var tempoConsumido = DateTime.Now - _dataInicio;
            var tempoTotal = _dataFim - _dataInicio;

            if (tempoConsumido > (tempoTotal/2))
                return _valorInicial * 0.85m;

            if (DateTime.Now.AddMonths(3) >= _dataFim)
                return _valorInicial * 0.94m;

            return _valorInicial * 0.70m;
        }
    }
}