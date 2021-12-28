using IbanValidator.Interfaces;
using System.Numerics;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class BelgiumBbanValidator : IBbanValidator
    {
        private readonly int _bbanCheckDigitLength = 2;
        private readonly int _bbanCheckDigitStartPosition = 14;
        private readonly int _bbanSubstringLength = 10;
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

            if (BigInteger.TryParse(bbanSubstring, out var bban) && int.TryParse(bbanCheckDigit, out var checkDigit))
            {
                var remainder = bban % _modulo;

                remainder = remainder == 0 ? _modulo : remainder;

                return Task.FromResult(remainder == checkDigit);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
