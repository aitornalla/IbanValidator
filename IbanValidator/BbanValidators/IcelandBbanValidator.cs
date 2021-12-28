using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class IcelandBbanValidator : IBbanValidator
    {
        private readonly int _bbanSubstringLength = 9;
        private readonly int _bbanSubstringStartPosition = 16;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 } };
        private readonly int _modulo = 11;
        private readonly int[] _weights = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public IcelandBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
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
