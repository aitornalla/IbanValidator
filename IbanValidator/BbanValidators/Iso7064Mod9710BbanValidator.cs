using IbanValidator.Algorithms;
using IbanValidator.Interfaces;
using System.Threading.Tasks;

namespace IbanValidator.BbanValidators
{
    internal class Iso7064Mod9710BbanValidator : IBbanValidator
    {
        private readonly int _bbanSubstringStartPosition = 4;

        private readonly Iso7064Mod9710Algorithm _iso7064Mod9710Algorithm;

        public Iso7064Mod9710BbanValidator(Iso7064Mod9710Algorithm iso7064Mod9710Algorithm)
        {
            _iso7064Mod9710Algorithm = iso7064Mod9710Algorithm;
        }

        public bool Validate(string iban)
        {
            return ValidateAsync(iban).Result;
        }

        public async Task<bool> ValidateAsync(string iban)
        {
            var bbanSubstring = iban.Substring(_bbanSubstringStartPosition);

            return await _iso7064Mod9710Algorithm.CheckAsync(bbanSubstring);
        }
    }
}
