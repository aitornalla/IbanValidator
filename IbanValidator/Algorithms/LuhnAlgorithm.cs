using System.Threading.Tasks;

namespace IbanValidator.Algorithms
{
    internal class LuhnAlgorithm
    {
        private readonly int _modulo10 = 10;

        public bool Check(string s)
        {
            return CheckAsync(s).Result;
        }

        public Task<bool> CheckAsync(string s)
        {
            var checkDigit = s.Substring(s.Length - 1);
            var substring = s.Substring(0, s.Length - 1);

            int totalSum = 0;

            for (int i = 0; i < substring.Length; i++)
            {
                if (int.TryParse(substring[substring.Length - 1 - i].ToString(), out var value))
                {
                    if (i % 2 == 0)
                        value *= 2;

                    totalSum += value >= 10 ? value - 9 : value;
                }
                else
                {
                    return Task.FromResult(false);
                }
            }

            if (int.TryParse(checkDigit, out var checkDigitValue))
            {
                var rest = totalSum % _modulo10;

                return Task.FromResult(rest == 0 ?
                    rest == checkDigitValue :
                    10 - rest == checkDigitValue);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
