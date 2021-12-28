using System.Threading.Tasks;

namespace IbanValidator.Interfaces
{
    internal interface IBbanValidator
    {
        bool Validate(string iban);
        Task<bool> ValidateAsync(string iban);
    }
}
