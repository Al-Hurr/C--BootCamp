
namespace d02_ex00.Models
{
    struct ExchangeSum
    {
        public CurrencyType CurrencyType;
        public double Amount;

        public override string ToString()
        {
            return $"{Amount:f} {GetCurrencyTypeStr()}";
        }

        public string GetCurrencyTypeStr()
        {
            return CurrencyType switch
            {
                CurrencyType.EUR => "EUR",
                CurrencyType.RUB => "RUB",
                CurrencyType.USD => "USD"
            };
        }
    }

    enum CurrencyType
    {
        EUR,
        RUB,
        USD
    }
}
