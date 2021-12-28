using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class TunisiaBbanValidator : IBbanValidator
    {
        private readonly int _bbanCheckDigitLength = 2;
        private readonly int _bbanCheckDigitStartPosition = 22;
        private readonly int _bbanSubstringLength = 18;
        private readonly int _bbanSubstringStartPosition = 4;
        private readonly int _modulo = 97;

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public Task<bool> ValidateAsync(string iban)
        {
            var bbanSubstring = iban.Substring(_bbanSubstringStartPosition, _bbanSubstringLength);
            var bbanCheckDigit = iban.Substring(_bbanCheckDigitStartPosition, _bbanCheckDigitLength);

            if (!BigInteger.TryParse($"{bbanSubstring}00", out var value) ||
                !int.TryParse(bbanCheckDigit, out var checkDigit) ||
                (_modulo - (value % _modulo) != checkDigit))
                return Task.FromResult(false);

            if (!BigInteger.TryParse(iban.Substring(_bbanSubstringStartPosition), out var value2) ||
                (value2 % _modulo) != 0)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
