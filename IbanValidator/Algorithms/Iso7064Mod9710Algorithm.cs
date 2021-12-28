using System.Numerics;
using System.Threading.Tasks;

namespace IbanValidator.Algorithms
{
    internal class Iso7064Mod9710Algorithm
    {
        private readonly int _modulo = 97;
        private readonly int _moduloSubstraction = 98;

        public bool Check(string s)
        {
            return CheckAsync(s).Result;
        }

        public Task<bool> CheckAsync(string s)
        {
            var checkDigits = s.Substring(s.Length - 2);
            var substring = s.Substring(0, s.Length - 2);

            if (BigInteger.TryParse($"{substring}00", out var bban) && int.TryParse(checkDigits, out var checkDigit))
            {
                return Task.FromResult(_moduloSubstraction - (bban % _modulo) == checkDigit);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
