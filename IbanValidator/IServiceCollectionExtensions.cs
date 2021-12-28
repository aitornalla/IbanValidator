using IbanValidator.Algorithms;
using IbanValidator.BbanValidators;
using IbanValidator.Interfaces;
using IbanValidator.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IbanValidator
{
    public static class IServiceCollectionExtensions
    {
        public static void AddIbanValidator(this IServiceCollection services, IConfiguration ibanValidatorConfiguration)
        {
            // Bind Iban validator configuration
            services.Configure<IbanValidatorConfiguration>(ibanValidatorConfiguration);
            // Services
            services.AddTransient<IIbanValidator, IbanValidator>();
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
        }
    }
}
