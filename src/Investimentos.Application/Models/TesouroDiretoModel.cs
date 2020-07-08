using System;
using Investimentos.Application.Models.ValueObjects;

namespace Investimentos.Application.Models
{
    public class TesouroDiretoModel
    {
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataDeCompra { get; set; }
        public decimal Iof { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public const decimal TaxaRentabilidade = 0.10M;
        private IR? _ir;
        public IR Ir
        {
            get
            {
                if (_ir is null)
                    _ir = new IR(ValorTotal, ValorInvestido, TaxaRentabilidade);

                return _ir.Value;
            }
        }
        private ValorResgate? _value;
        public ValorResgate ValorResgate
        {
            get 
            {
                if (_value is null)
                {
                    _value = new ValorResgate(DataDeCompra, Vencimento, ValorTotal);
                }

                return _value.Value;
            }
        }
    }

}