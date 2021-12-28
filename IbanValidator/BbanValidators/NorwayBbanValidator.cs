using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class NorwayBbanValidator : IBbanValidator
    {
        private readonly int _bbanSubstringStartPosition = 4;
        private readonly Dictionary<int, int> _complements = new Dictionary<int, int> { { 0, 0 } };
        private readonly int _modulo = 11;
        private readonly string _substringZeroCheck = "00";
        private readonly int _substringZeroCheckLength = 2;
        private readonly int _substringZeroCheckStartPosition = 4;
        private readonly int _substringZeroStartPosition = 6;
        private readonly int[] _weights = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

        private readonly WeightedModuloAlgorithm _weightedModuloAlgorithm;

        public NorwayBbanValidator(WeightedModuloAlgorithm weightedModuloAlgorithm)
        {
            _weightedModuloAlgorithm = weightedModuloAlgorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanBankCodeSubstring = iban.Substring(_bbanSubstringStartPosition);

            if (bbanBankCodeSubstring.Substring(_substringZeroCheckStartPosition, _substringZeroCheckLength).Equals(_substringZeroCheck))
                bbanBankCodeSubstring = bbanBankCodeSubstring.Substring(_substringZeroStartPosition);

            return await _weightedModuloAlgorithm.CheckAsync(bbanBankCodeSubstring, _modulo, _complements, _weights);
        }
    }
}
