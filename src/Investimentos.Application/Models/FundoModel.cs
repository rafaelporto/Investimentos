using System;
using Investimentos.Application.Models.ValueObjects;

namespace Investimentos.Application.Models
{
    public class FundoModel
    {
        public decimal CapitalInvestido { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal Iof { get; set; }
        public decimal TotalTaxas { get; set; }
        public decimal Quantity { get; set; }
        public DateTime DataResgate { get; set; }
        public DateTime DataCompra { get; set; }
        public string Nome { get; set; }
        public const decimal TaxaRentabilidade = 0.05m;
        private IR? _ir;
        public IR Ir
        {
            get
            {
                if (_ir is null)
                    _ir = new IR(ValorAtual, CapitalInvestido, TaxaRentabilidade);

                return _ir.Value;
            }
        }
        private ValorResgate? _valorResgate;
        public ValorResgate ValorResgate
        {
            get
            {
                if (_valorResgate is null)
                {
                    _valorResgate = new ValorResgate(DataCompra, DataResgate, ValorAtual);
                }

                return _valorResgate.Value;
            }
        }
    }
}