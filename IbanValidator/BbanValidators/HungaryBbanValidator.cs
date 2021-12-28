using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class HungaryBbanValidator : IBbanValidator
    {
        private readonly int _bbanAccountNumberSubstringStartPosition = 12;
        private readonly int _bbanAccountPrefixSubstringLength = 8;
        private readonly int _bbanAccountPrefixSubstringStartPosition = 4;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 } };
        private readonly int _modulo = 10;
        private readonly int[] _weigths = new int[] { 9, 7, 3, 1, 9, 7, 3, 1, 9, 7, 3, 1, 9, 7, 3, 1 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public HungaryBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
        {
            _weightedModuloAlgorithm = weightedModuloAlgorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanAccountPrefixSubstring = iban.Substring(_bbanAccountPrefixSubstringStartPosition, _bbanAccountPrefixSubstringLength);
            var bbanAccountNumberSubstring = iban.Substring(_bbanAccountNumberSubstringStartPosition);

            return
                await _weightedModuloAlgorithm.CheckAsync(bbanAccountPrefixSubstring, _modulo, _complements, _weigths) &&
                await _weightedModuloAlgorithm.CheckAsync(bbanAccountNumberSubstring, _modulo, _complements, _weigths);
        }
    }
}
