using System.Threading.Tasks;

namespace IbanValidator.Algorithms
{
    internal class Iso7064Mod1110Algorithm
    {
        private readonly int _modulo10 = 10;
        private readonly int _modulo11 = 11;

        public bool Check(string s)
        {
            return CheckAsync(s).Result;
        }

        public Task<bool> CheckAsync(string s)
        {
            int rest = 0;

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (int.TryParse(s[i].ToString(), out var digit))
                {
                    int sum = i == 0 ? digit + 10 : digit + rest;

                    int subtotal = sum % _modulo10;

                    subtotal = subtotal == 0 ? 10 : subtotal;

                    rest = (subtotal * 2) % _modulo11;
                }
                else
                {
                    return Task.FromResult(false);
                }
            }

            if (int.TryParse(s[s.Length - 1].ToString(), out var checkDigit))
            {
                return Task.FromResult(
                    rest == 1 ?
                    0 == checkDigit :
                    11 - rest == checkDigit);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
