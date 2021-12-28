using IbanValidator.Models;
using System.Threading.Tasks;

namespace IbanValidator.Interfaces
{
    public interface IIbanValidator
    {
        IbanValidatorResult Validate(string iban, bool validateBban = false);
        Task<IbanValidatorResult> ValidateAsync(string iban, bool validateBban = false);
    }
}
