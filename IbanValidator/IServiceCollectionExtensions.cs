using IbanValidator;
using IbanValidator.Algorithms;
using IbanValidator.BbanValidators;
using IbanValidator.Interfaces;
using IbanValidator.Models;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddIbanValidator(this IServiceCollection services, IConfiguration ibanValidatorConfiguration)
        {
            // Services
            services.AddTransient<IIbanValidator, IbanValidator.IbanValidator>();
            services.AddTransient<IIbanValidationService, IbanValidationService>();
            services.AddTransient<IBbanValidatorServiceResolver, BbanValidatorServiceResolver>();
            // Algorithms
            services.AddTransient<Iso7064Mod1110Algorithm>();
            services.AddTransient<Iso7064Mod9710Algorithm>();
            services.AddTransient<LuhnAlgorithm>();
            services.AddTransient<WeightedModuloAlgorithm>();
            // Bban validators
            services.AddTransient<AlbaniaBbanValidator>();
            services.AddTransient<BelgiumBbanValidator>();
            services.AddTransient<CroatiaBbanValidator>();
            services.AddTransient<CzechRepublicSlovakiaBbanValidator>();
            services.AddTransient<EstoniaBbanValidator>();
            services.AddTransient<FinlandBbanValidator>();
            services.AddTransient<FranceBbanValidator>();
            services.AddTransient<HungaryBbanValidator>();
            services.AddTransient<IcelandBbanValidator>();
            services.AddTransient<Iso7064Mod9710BbanValidator>();
            services.AddTransient<ItalyBbanValidator>();
            services.AddTransient<NetherlandsBbanValidator>();
            services.AddTransient<NorwayBbanValidator>();
            services.AddTransient<PolandBbanValidator>();
            services.AddTransient<SpainBbanValidator>();
            services.AddTransient<TunisiaBbanValidator>();
            // Bind Iban validator configuration
            services.Configure<IbanValidatorConfiguration>(ibanValidatorConfiguration);

            return services;
        }
    }
}
