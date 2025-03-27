using AITravelAgent;

class CurrencyConverter
{
    [KernelFunction, 
    Description("Convert an amount from one currency to another")]
    public static string ConvertAmount()
    {
        var currencyDictionary = Currency.Currencies;
    }
}
