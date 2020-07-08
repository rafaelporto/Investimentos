using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Investimentos.Api.Models
{
    ///<summary>
    /// Objeto de retorno default da API quando ocorre algum erro no processo da request
    ///</summary>
    public class ApiBadResult
    {
        [JsonIgnore]
        private string[] _errors;

        ///<summary>
        /// - Lista de erros da operação executada
        /// - Campo do tipo array de strings
        ///</summary>
        ///<example> Ocorreu um erro no processamento da requisição</example>
        public IReadOnlyCollection<string> Errors => _errors;
        
        public ApiBadResult(IEnumerable<string> errors)
        {
            _errors = errors.ToArray();
        }

        public ApiBadResult(string error)
        {
            _errors = new string[] { error };
        }
    }
}