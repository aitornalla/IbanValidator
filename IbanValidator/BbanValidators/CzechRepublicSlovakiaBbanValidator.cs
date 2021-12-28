using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class CzechRepublicSlovakiaBbanValidator : IBbanValidator
    {
        private readonly int _bbanAccountNumberSubstringStartPosition = 14;
        private readonly int _bbanAccountPrefixSubstringLength = 6;
        private readonly int _bbanAccountPrefixSubstringStartPosition = 8;
        private readonly int _bbanAccountPrefixWeightStart = 4;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 } };
        private readonly int _modulo = 11;
        private readonly int[] _weigths = new int[] { 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public CzechRepublicSlovakiaBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
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
                await _weightedModuloAlgorithm.CheckAsync(bbanAccountNumberSubstring, _modulo, _complements, _weigths);
        }
    }
}
