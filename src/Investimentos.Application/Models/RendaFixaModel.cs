using System;
using Investimentos.Application.Models.ValueObjects;

namespace Investimentos.Application.Models
{
    public class RendaFixaModel
    {
        public decimal CapitalInvestido { get; set; }
        public decimal CapitalAtual { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Iof { get; set; }
        public decimal OutrasTaxas { get; set; }
        public decimal Taxas { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataOperacao { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public bool GuarantidoFGC { get; set; }
        public bool Primario { get; set; }
        public const decimal TaxaRentabilidade = 0.05M;
        private IR? _ir;
        public IR Ir
        {
            get
            {
                if (_ir is null)
                    _ir = new IR(CapitalAtual, CapitalInvestido, TaxaRentabilidade);

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
                    _value = new ValorResgate(DataOperacao, Vencimento, CapitalAtual);
                }

                return _value.Value;
            }
        }
    }
}