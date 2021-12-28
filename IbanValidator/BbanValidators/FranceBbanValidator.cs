using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class FranceBbanValidator : IBbanValidator
    {
        private readonly int _bbanCheckDigitsLength = 2;
        private readonly int _bbanCheckDigitsStartPosition = 25;
        private readonly int _bbanSubstringLength = 21;
        private readonly int _bbanSubstringStartPosition = 4;
        private readonly int _modulo = 97;
        private readonly Dictionary<char, char> _transformDictionary = new Dictionary<char, char>
        {
            { 'A', '1' }, { 'B', '2' }, { 'C', '3' }, { 'D', '4' },
            { 'E', '5' }, { 'F', '6' }, { 'G', '7' }, { 'H', '8' },
            { 'I', '9' }, { 'J', '1' }, { 'K', '2' }, { 'L', '3' },
            { 'M', '4' }, { 'N', '5' }, { 'O', '6' }, { 'P', '7' },
            { 'Q', '8' }, { 'R', '9' }, { 'S', '2' }, { 'T', '3' },
            { 'U', '4' }, { 'V', '5' }, { 'W', '6' }, { 'X', '7' },
            { 'Y', '8' }, { 'Z', '9' }
        };

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public Task<bool> ValidateAsync(string iban)
        {
            var bbanSubstring = iban.Substring(_bbanSubstringStartPosition, _bbanSubstringLength);
            var bbanCheckDigits = iban.Substring(_bbanCheckDigitsStartPosition, _bbanCheckDigitsLength);

            for (int i = 0; i < bbanSubstring.Length; i++)
            {
                if (_transformDictionary.TryGetValue(bbanSubstring[i], out var replaceDigit))
                {
                    bbanSubstring = bbanSubstring.Remove(i, 1).Insert(i, replaceDigit.ToString());
                }
            }

            if (BigInteger.TryParse($"{bbanSubstring}00", out var value) && int.TryParse(bbanCheckDigits, out var checkDigitsValue))
            {
                return Task.FromResult(_modulo - (value % _modulo) == checkDigitsValue);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
