using System.Collections.Generic;

namespace Investimentos.Application.Models
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }

        public IList<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                Succeeded = true,
                Data = data
            };
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>
            {
                Errors = (IList<string>)errors
            };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>
            {
                Errors = new List<string>() { error }
            };
        }
    }
}