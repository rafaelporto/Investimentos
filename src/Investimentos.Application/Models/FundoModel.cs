using System;

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
    }
}