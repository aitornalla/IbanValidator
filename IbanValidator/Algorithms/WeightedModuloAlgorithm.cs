using System.Collections.Generic;
using System.Threading.Tasks;

namespace IbanValidator.Algorithms
{
    internal class WeightedModuloAlgorithm
    {
        public bool Check(string s, int modulo, Dictionary<int, int> complements, int[] weights, int weightStart = 0)
        {
            return CheckAsync(s, modulo, complements, weights, weightStart).Result;
        }

        public Task<bool> CheckAsync(string s, int modulo, Dictionary<int, int> complements, int[] weights, int weightStart = 0)
        {
            var checkDigit = s.Substring(s.Length - 1);
            var substring = s.Substring(0, s.Length - 1);

            int totalSum = 0;

            for (int i = 0; i < substring.Length; i++)
            {
                if (int.TryParse(substring[i].ToString(), out var value))
                {
                    totalSum += value * weights[i + weightStart];
                }
                else
                {
                    return Task.FromResult(false);
                }
            }

            if (int.TryParse(checkDigit, out var checkDigitValue))
            {
                var rest = totalSum % modulo;

                if (complements.TryGetValue(rest, out var complement))
                {
                    return Task.FromResult(complement == checkDigitValue);
                }
                else
                {
                    return Task.FromResult((modulo - rest) == checkDigitValue);
                }
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
