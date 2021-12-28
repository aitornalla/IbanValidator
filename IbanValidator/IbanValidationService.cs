using IbanValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace IbanValidator
{
    internal class IbanValidationService : IIbanValidationService
    {
        public bool HasNotPermittedCharacters(string iban, char[] permittedCharacters)
        {
            foreach (var iChar in iban)
            {
                if (!permittedCharacters.Contains(iChar))
                {
                    if (!(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }).Contains(iChar))
                        return true;
                }
            }

            return false;
        }

        public bool IsIbanValid(string iban, Dictionary<string, int> permittedCharacterValues)
        {
            var sbIban = new StringBuilder(iban.Substring(4) + iban.Substring(0, 4));

            int i = 0;

            do
            {
                var stringIban = sbIban[i].ToString();

                if (permittedCharacterValues.TryGetValue(stringIban, out var value))
                {
                    var sValue = value.ToString();

                    sbIban.Replace(stringIban, sValue, i, 1);

                    i += sValue.Length;
                }
                else
                {
                    i++;
                }

            } while (i < sbIban.Length);

            if (BigInteger.TryParse(sbIban.ToString(), out var numberIban))
            {
                return numberIban % 97 == 1;
            }
            else
            {
                return false;
            }
        }

        public bool IsLengthCorrectForCountry(int ibanLength, int ibanCountryLength)
        {
            return ibanLength == ibanCountryLength;
        }

        public bool IsLengthWithinLimits(string iban, int minLength, int maxLength)
        {
            return (iban.Length > minLength && iban.Length < maxLength);
        }

        public bool IsRegexMatchForCountry(string iban, string ibanCountryPattern)
        {
            var rgx = new Regex(ibanCountryPattern);

            return rgx.IsMatch(iban);
        }

        public string PrepareIban(string iban)
        {
            return (string.Join(string.Empty, iban.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries))).ToUpperInvariant();
        }
    }
}
