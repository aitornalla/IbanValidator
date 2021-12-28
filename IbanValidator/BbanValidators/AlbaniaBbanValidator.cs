using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class AlbaniaBbanValidator : IBbanValidator
    {
        private readonly int _bbanBankCodeSubstringLength = 8;
        private readonly int _bbanBankCodeSubstringStartPosition = 4;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 } };
        private readonly int _modulo = 10;
        private readonly int[] _weights = new int[] { 9, 7, 3, 1, 9, 7, 3, 1 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public AlbaniaBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
        {
            _weightedModuloAlgorithm = weightedModuloAlgorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanBankCodeSubstring = iban.Substring(_bbanBankCodeSubstringStartPosition, _bbanBankCodeSubstringLength);

            return await _weightedModuloAlgorithm.CheckAsync(bbanBankCodeSubstring, _modulo, _complements, _weights);
        }
    }
}
