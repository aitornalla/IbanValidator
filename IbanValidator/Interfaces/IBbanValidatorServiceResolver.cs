using System;
using System.Threading.Tasks;

namespace IbanValidator.Interfaces
{
    internal interface IBbanValidatorServiceResolver
    {
        IBbanValidator Resolve(Type type);
        Task<IBbanValidator> ResolveAsync(Type type);
    }
}
