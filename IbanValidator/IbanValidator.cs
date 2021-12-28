using IbanValidator.Interfaces;
using IbanValidator.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IbanValidator
{
    internal class IbanValidator : IIbanValidator
    {
        private readonly IBbanValidatorServiceResolver _bbanValidatorServiceResolver;
        private readonly IIbanValidationService _ibanValidationService;

        private readonly IbanValidatorConfiguration _ibanValidatorConfiguration;

        public IbanValidator(
            IBbanValidatorServiceResolver bbanValidatorServiceResolver,
            IIbanValidationService ibanValidationService,
            IOptions<IbanValidatorConfiguration> ibanValidatorOptions)
        {
            _bbanValidatorServiceResolver = bbanValidatorServiceResolver;
            _ibanValidationService = ibanValidationService;

            _ibanValidatorConfiguration = ibanValidatorOptions?.Value ?? throw new ArgumentNullException(nameof(ibanValidatorOptions), "There are no IbanValidator settings configured");
        }

        public IbanValidatorResult Validate(string iban, bool validateBban = false)
        {
            return ValidateAsync(iban, validateBban).Result;
        }

        public async Task<IbanValidatorResult> ValidateAsync(string iban, bool validateBban = false)
        {
            // Validation - null or empty
            if (string.IsNullOrEmpty(iban))
                return new IbanValidatorResult { Success = false, Error = "Iban is null or empty" };
            // Prepare iban
            var vIban = _ibanValidationService.PrepareIban(iban);
            // Validation - length within limits
            if (!_ibanValidationService.IsLengthWithinLimits(vIban, _ibanValidatorConfiguration.MinimumLength, _ibanValidatorConfiguration.MaximumLength))
                return new IbanValidatorResult { Success = false, Error = "Not valid iban length" };
            // Validation - permitted characters
            if (_ibanValidationService.HasNotPermittedCharacters(vIban, _ibanValidatorConfiguration.PermittedCharacterValues.Keys.Select(s => s.ToCharArray()[0]).ToArray()))
                return new IbanValidatorResult { Success = false, Error = "Iban has non-permitted characters" };
            // Get iban country settings
            var ibanCountrySettings = _ibanValidatorConfiguration.IbanCountrySettings.FirstOrDefault(f => f.CountryCode.Equals(vIban.Substring(0, 2)));
            // Validation - null iban country settings
            if (ibanCountrySettings == null)
                return new IbanValidatorResult { Success = false, Error = $"Iban country code '{vIban.Substring(0, 2)}' not found" };
            // Validation - country length
            if (!_ibanValidationService.IsLengthCorrectForCountry(vIban.Length, ibanCountrySettings.IbanLength))
                return new IbanValidatorResult { Success = false, Error = $"Not valid iban length for country code '{ibanCountrySettings.CountryCode}'" };
            // Validation - country pattern
            if (!_ibanValidationService.IsRegexMatchForCountry(vIban, ibanCountrySettings.IbanRegexExpression))
                return new IbanValidatorResult { Success = false, Error = "Iban does not match iban country pattern" };
            // Validation - mod 97
            if (!_ibanValidationService.IsIbanValid(vIban, _ibanValidatorConfiguration.PermittedCharacterValues))
                return new IbanValidatorResult { Success = false, Error = "Not valid Iban" };
            // Validation - Bban
            if (validateBban)
            {
                if (string.IsNullOrEmpty(ibanCountrySettings.BbanValidatorFullName))
                    return new IbanValidatorResult { Success = false, Error = $"Bban validator not configured for this Iban country code '{ibanCountrySettings.CountryCode}'" };

                var bbanValidator = await _bbanValidatorServiceResolver.ResolveAsync(Type.GetType(ibanCountrySettings.BbanValidatorFullName));

                if (bbanValidator == null)
                    return new IbanValidatorResult { Success = false, Error = $"Bban validator '{ibanCountrySettings.BbanValidatorFullName}' not found" };

                if (!(await bbanValidator.ValidateAsync(vIban)))
                    return new IbanValidatorResult { Success = false, Error = "Not valid Bban" };
            }

            return new IbanValidatorResult { Success = true };
        }
    }
}
