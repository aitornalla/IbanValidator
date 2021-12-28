using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class SpainBbanValidator : IBbanValidator
    {
        private readonly int _bbanAccountNumberSubstringStartPosition = 13;
        private readonly int _bbanAccountPrefixSubstringLength = 9;
        private readonly int _bbanAccountPrefixSubstringStartPosition = 4;
        private readonly int _bbanAccountPrefixWeightStart = 2;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 }, { 1, 1 } };
        private readonly int _modulo = 11;
        private readonly int[] _weigths = new int[] { 1, 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public SpainBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
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
                await _weightedModuloAlgorithm.CheckAsync(bbanAccountPrefixSubstring, _modulo, _complements, _weigths, _bbanAccountPrefixWeightStart) &&
                await _weightedModuloAlgorithm.CheckAsync(bbanAccountNumberSubstring.Substring(1) + bbanAccountNumberSubstring.Substring(0, 1), _modulo, _complements, _weigths);
        }
    }
}
