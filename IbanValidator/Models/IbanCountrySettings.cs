namespace IbanValidator.Models
{
    internal class IbanCountrySettings
    {
        public string BbanValidatorFullName { get; set; }
        public string CountryCode { get; set; }
        public string IbanRegexExpression { get; set; }
        public int IbanLength { get; set; }
    }
}
