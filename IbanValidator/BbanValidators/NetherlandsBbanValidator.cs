using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class NetherlandsBbanValidator : IBbanValidator
    {
        private readonly int _bbanSubstringStartPosition = 8;
        private readonly int _modulo = 11;
        private readonly int[] _weights = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public Task<bool> ValidateAsync(string iban)
        {
            var bbanSubstring = iban.Substring(_bbanSubstringStartPosition);

            int totalSum = 0;

            for (int i = 0; i < bbanSubstring.Length; i++)
            {
                if (int.TryParse(bbanSubstring[i].ToString(), out var value))
                {
                    totalSum += value * _weights[i];
                }
                else
                {
                    return Task.FromResult(false);
                }
            }

            return Task.FromResult(totalSum % _modulo == 0);
        }
    }
}
