using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class FinlandBbanValidator : IBbanValidator
    {
        private readonly int _bbanSubstringStartPosition = 4;

        private readonly LuhnAlgorithm _luhnAlgorithm;

        public FinlandBbanValidator(LuhnAlgorithm luhnAlgorithm)
        {
            _luhnAlgorithm = luhnAlgorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanBankCodeSubstring = iban.Substring(_bbanSubstringStartPosition);

            return await _luhnAlgorithm.CheckAsync(bbanBankCodeSubstring);
        }
    }
}
