using System;

namespace Investimentos.Application.Models
{
    public class InvestimentoModel
    {
        ///<summary>
        /// - Nome no investimento
        /// - Campo do tipo string
        ///</summary>
        ///<example>Tesouro Selic 2025</example>
        public string Nome { get; set; }

        ///<summary>
        /// - Valor investido no investimento do cliente
        /// - Campo do tipo decimal
        ///</summary>
        ///<example>2799.472</example>
        public decimal ValorInvestido { get; set; }

        ///<summary>
        /// - Valor total do investimento do cliente
        /// - Casmpo do tipo decimal
        ///</summary>
        ///<example>829.68</example>
        public decimal ValorTotal { get; set; }

        ///<summary>
        /// - Data de vencimento do investimento do cliente
        /// - Campo do tipo DateTime
        ///</summary>
        ///<example>2025-03-01T00:00:00</example>
        public DateTime Vencimento { get; set; }

        ///<summary>
        /// - Valor do IR do investimento do cliente
        /// - Campo do tipo decimal
        ///</summary>
        ///<example>3.0208</example>
        public decimal Ir { get; set; }

        ///<summary>
        /// - Valor a ser resgatado do investimento do cliente
        /// - Campo do tipo decimal
        ///</summary>
        ///<example>705.228</example>
        public decimal ValorResgate { get ; set; }
    }
}