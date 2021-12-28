using System.Collections.Generic;

namespace IbanValidator.Models
{
    internal class IbanValidatorConfiguration
    {
        public IbanCountrySettings[] IbanCountrySettings { get; set; }
        public int MaximumLength { get; set; }
        public int MinimumLength { get; set; }
        public Dictionary<string, int> PermittedCharacterValues { get; set; }
    }
}
