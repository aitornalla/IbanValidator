using System;
using System.Threading.Tasks;

namespace IbanValidator.Interfaces
{
    internal interface IBbanValidatorServiceResolver
    {
        IBbanValidator Resolve(Type type);
        IBbanValidator Resolve<T>() where T : IBbanValidator;
        Task<IBbanValidator> ResolveAsync(Type type);
        Task<IBbanValidator> ResolveAsync<T>() where T : IBbanValidator;
    }
}
