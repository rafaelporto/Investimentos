namespace Investimentos.Application.Configuration
{
    public class ApplicationOptions
    {
        public string BaseAddress { get; set; }
        public string TesouroDiretoEndpoint { get; set; }
        public string RendaFixaEndpoint { get; set; }
        public string FundosEndpoint { get; set; }

        public string GetTesouroDireto(string version = "2")
        {
            return $"{BaseAddress}/v{version}/{TesouroDiretoEndpoint}";
        }

        public string GetRendaFixa(string version = "2")
        {
            return $"{BaseAddress}/v{version}/{RendaFixaEndpoint}";
        }

        public string GetFundos(string version = "2")
        {
            return $"{BaseAddress}/v{version}/{FundosEndpoint}";
        }
    }
}