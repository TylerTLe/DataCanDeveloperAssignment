namespace DataCon.Models
{
    public static class CurrencyConverter
    {
        private static readonly Dictionary<string, decimal> ExchangeRates = new Dictionary<string, decimal>
    {
        {"USD", 0.5m},
        {"MXN", 10m},
        {"EURO", 0.25m},
        {"CAD", 1m}
    };


        public static decimal ConvertToCAD(decimal amount, string currency)
        {
            if (ExchangeRates.ContainsKey(currency.ToUpper()))
            {
                return amount / ExchangeRates[currency.ToUpper()];
            }
            throw new ArgumentException("Unsupported currency.");
        }
    }
}
