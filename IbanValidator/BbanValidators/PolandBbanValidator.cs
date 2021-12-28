using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class PolandBbanValidator : IBbanValidator
    {
        private readonly int _bbanSubstringLength = 8;
        private readonly int _bbanSubstringStartPosition = 4;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 } };
        private readonly int _modulo = 10;
        private readonly int[] _weights = new int[] { 3, 9, 7, 1, 3, 9, 7 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public PolandBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
        {
            _weightedModuloAlgorithm = weightedModuloAlgorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanBankCodeSubstring = iban.Substring(_bbanSubstringStartPosition, _bbanSubstringLength);

            return await _weightedModuloAlgorithm.CheckAsync(bbanBankCodeSubstring, _modulo, _complements, _weights);
        }
    }
}
