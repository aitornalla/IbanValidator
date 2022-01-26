using IbanValidator.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IbanValidator
{
    internal class BbanValidatorServiceResolver : IBbanValidatorServiceResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public BbanValidatorServiceResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBbanValidator Resolve(Type type)
        {
            return ResolveAsync(type).Result;
        }

        public IBbanValidator Resolve<T>() where T : IBbanValidator
        {
            return ResolveAsync<T>().Result;
        }

        public Task<IBbanValidator> ResolveAsync(Type type)
        {
            if (type == null || !type.GetInterfaces().Contains(typeof(IBbanValidator)))
            {
                return Task.FromResult(null as IBbanValidator);
            }

            return Task.FromResult(_serviceProvider.GetService(type) as IBbanValidator);
        }

        public Task<IBbanValidator> ResolveAsync<T>() where T : IBbanValidator
        {
            return Task.FromResult(_serviceProvider.GetRequiredService<T>() as IBbanValidator);
        }
    }
}
