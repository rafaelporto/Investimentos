using System;

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
        public DateTimeOffset Vencimento { get; set; }
        public DateTimeOffset DataOperacao { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public bool GuarantidoFGC { get; set; }
        public bool Primario { get; set; }
    }
}