using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class CroatiaBbanValidator : IBbanValidator
    {
        private readonly int _bbanAccountNumberSubstringStartPosition = 11;
        private readonly int _bbanBankCodeSubstringLength = 7;
        private readonly int _bbanBankCodeSubstringStartPosition = 4;

        private readonly Iso7064Mod1110Algorithm _iso7064Mod1110Algorithm;

        public CroatiaBbanValidator(Iso7064Mod1110Algorithm iso7064Mod1110Algorithm)
        {
            _iso7064Mod1110Algorithm = iso7064Mod1110Algorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanBankCodeSubstring = iban.Substring(_bbanBankCodeSubstringStartPosition, _bbanBankCodeSubstringLength);
            var bbanAccountNumberSubstring = iban.Substring(_bbanAccountNumberSubstringStartPosition);

            return await _iso7064Mod1110Algorithm.CheckAsync(bbanBankCodeSubstring) && await _iso7064Mod1110Algorithm.CheckAsync(bbanAccountNumberSubstring);
        }
    }
}
