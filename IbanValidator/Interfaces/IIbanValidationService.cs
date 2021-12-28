using System.Collections.Generic;

namespace IbanValidator.Interfaces
{
    internal interface IIbanValidationService
    {
        bool HasNotPermittedCharacters(string iban, char[] permittedCharacters);
        bool IsIbanValid(string iban, Dictionary<string, int> permittedCharacterValues);
        bool IsLengthCorrectForCountry(int ibanLength, int ibanCountryLength);
        bool IsLengthWithinLimits(string iban, int minLength, int maxLength);
        bool IsRegexMatchForCountry(string iban, string ibanCountryPattern);
        string PrepareIban(string iban);
    }
}
