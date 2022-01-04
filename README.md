# IbanValidator

## Table of contents
* [General info](#general-info)
* [Features](#features)
* [Supported countries](#supported-countries)
* [IIbanValidator service](#iibanvalidator-service)
* [IbanValidatorResult return response](#ibanvalidatorresult-return-response)
* [Return errors](#return-errors)
* [Bank account validations](#bank-account-validations)
* [How to use the library in an ASP.NET Core project](#how-to-use-the-library-in-an-aspnet-core-project)
* [Repository example](#repository-example)

## General info
This project contains a .NET Standard 2.0 class library for validating Iban numbers. This class library is intended to be used in an ASP.NET Core project as a service injected by DI, but can be modified to be used in any type of project.

## Features
* Length limits validations
* Permitted characters validation
* Country pattern validation
* [ISO 7064](https://en.wikipedia.org/wiki/ISO/IEC_7064) validation
* Bank account validations (for some countries)

## Supported countries
The following table shows the countries included in the project configuration and whos iban numbers can be validated:
| Country | Code | Bank account validations |
| :------- | :----: | :------------------------: |
| Andorra | AD | - |
| United Arab Emirates | AE | - |
| Albania | AL | Yes |
| Austria | AT | - |
| Azerbaijan | AZ | - |
| Bosnia and Herzegovina | BA | Yes |
| Belgium | BE | Yes |
| Bulgaria | BG | - |
| Bahrain | BH | - |
| Saint Barthélemy | BL | Yes |
| Brazil | BR | - |
| Belarus | BY | - |
| Switzerland | CH | - |
| Costa Rica | CR | - |
| Cyprus | CY | - |
| Czech Republic | CZ | Yes |
| Germany | DE | - |
| Denmark | DK | - |
| Dominican Republic | DO | - |
| Estonia | EE | Yes |
| Egypt | EG | - |
| Spain | ES | Yes |
| Finland | FI | Yes |
| Faroe Islands | FO | - |
| France | FR | Yes |
| United Kingdom | GB | - |
| Georgia | GE | - |
| French Guiana | GF | Yes |
| Gibraltar | GI | - |
| Greenland | GL | - |
| Guadeloupe | GP | Yes |
| Greece | GR | - |
| Guatemala | GT | - |
| Croatia | HR | Yes |
| Hungary | HU | Yes |
| Ireland | IE | - |
| Israel | IL | - |
| Iceland | IS | Yes |
| Italy | IT | Yes |
| Jordan | JO | - |
| Kuwait | KW | - |
| Kazakhstan | KZ | - |
| Lebanon | LB | - |
| Saint Lucia | LC | - |
| Liechtenstein | LI | - |
| Lithuania | LT | - |
| Luxembourg | LU | - |
| Latvia | LV | - |
| Monaco | MC | Yes |
| Moldova | MD | - |
| Montenegro | ME | Yes |
| Saint Martin | MF | Yes |
| North Macedonia | MK | Yes |
| Martinique | MQ | Yes |
| Mauritania | MR | Yes |
| Malta | MT | - |
| Mauritius | MU | - |
| New Caledonia | NC | Yes |
| Netherlands | NL | Yes |
| Norway | NO | Yes |
| French Polynesia | PF | Yes |
| Pakistan | PK | - |
| Poland | PL | Yes |
| Saint Pierre and Miquelon | PM | Yes |
| Palestine | PS | - |
| Portugal | PT | Yes |
| Qatar | QA | - |
| Réunion | RE | Yes |
| Romania | RO | - |
| Serbia | RS | Yes |
| Saudi Arabia | SA | - |
| Seychelles | SC | - |
| Sweden | SE | - |
| Slovenia | SI | Yes |
| Slovakia | SK | Yes |
| San Marino | SM | Yes |
| Sao Tome and Principe | ST | - |
| El Salvador | SV | - |
| French Southern and Antarctic Lands | TF | Yes |
| East Timor | TL | Yes |
| Tunisia | TN | Yes |
| Turkey | TR | - |
| Ukraine | UA | - |
| Vatican City | VA | - |
| Virgin Islands (British) | VG | - |
| Territory of the Wallis and Futuna Islands | WF | Yes |
| Kosovo | XK | Yes |
| Mayotte | YT | Yes |

## IIbanValidator service
The only exposed interface of the library is the `IIbanValidator` interface that implements two methods. These two methods are used to validate the iban number in a syncrhonous or asynchronous way:
```c#
public interface IIbanValidator
{
    IbanValidatorResult Validate(string iban, bool validateBban = false);
    Task<IbanValidatorResult> ValidateAsync(string iban, bool validateBban = false);
}
```
Both methods accept the iban number as the first parameter and a second optional boolean parameter to also validate the bank account number (only implemented for some countries, check the table above).

## IbanValidatorResult return response
The previous methods will return either `IbanValidatorResult` or `Task<IbanValidatorResult>` with the result of the validation:
```c#
public class IbanValidatorResult
{
    public bool Success { get; set; }
    public string Error { get; set; }
}
```

## Return errors
* "Iban is null or empty"
* "Not valid iban length"
* "Iban has non-permitted characters"
* "Iban country code '{countryCode}' not found"
* "Not valid iban length for country code '{countryCode}'"
* "Iban does not match iban country pattern"
* "Not valid Iban"
* "Bban validator not configured for this Iban country code '{countryCode}'" -> this error is returned when `validateBban` optional parameter is set to `true` but no bban validator is configured for the country iban number.
* "Bban validator '{bbanValidatorFullName}' not found" -> this error is returned when `validateBban` optional parameter is set to `true` and a bban validator has been specified in the configuration but this bban validator was not found.
* "Not valid Bban" -> this error is returned when `validateBban` optional parameter is set to `true` and the bban validator checks the bank account to be wrong.

## Bank account validations
Some countries include check algorithms to validate their bank account numbers. The following table shows the bank account validations implemented in the library and the country codes that implement them (check this [link](https://en.wikipedia.org/wiki/International_Bank_Account_Number#National_check_digits)):
| Validator | Type | Country codes |
| :--------- | :----: | :-------------: |
| AlbaniaBbanValidator | Weighted | AL |
| Iso7064Mod9710BbanValidator | ISO 7064 MOD-97-10 | BA, ME, MK, PT, RS, SI, TL, XK |
| BelgiumBbanValidator | ISO 7064 MOD-97-10 (Variant) | BE |
| FranceBbanValidator | ISO 7064 MOD-97-10 (variant) | BL, FR, GF, GP, MC, MF, MQ, MR, NC, PF, PM, RE, TF, WF, YT |
| CzechRepublicSlovakiaBbanValidator | Weighted | CZ, SK |
| EstoniaBbanValidator | Weighted | EE |
| SpainBbanValidator | Weighted | ES |
| FinlandBbanValidator | Luhn | FI |
| CroatiaBbanValidator | ISO 7064 MOD-11-10 | HR |
| HungaryBbanValidator | Weighted | HU |
| IcelandBbanValidator | Weighted | IS |
| ItalyBbanValidator | Conversion + Sum | IT, SM |
| NetherlandsBbanValidator | Weighted | NL |
| NorwayBbanValidator | Weighted | NO |
| PolandBbanValidator | Weighted | PL |
| TunisiaBbanValidator | ISO 7064 MOD-97-10 (variant) | TN |

## How to use the library in an ASP.NET Core project
* Add the project *IbanValidator* to your solution and add a reference to this project.
* Copy the *IbanValidatorSettings* section from this [json file](IbanValidatorApi/appsettings.json) to the *appsettings.json* file in your project.
* In the *Startup.cs* file of your project, add the service. You can load the `IConfiguration` parameter for the `AddIbanValidator` method from whatever source you use as long as it has the same data structure as in the *IbanValidatorSettings* section in this [json file](IbanValidatorApi/appsettings.json).
```c#
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddIbanValidator(Configuration.GetSection("IbanValidatorSettings"));
    ...
}
```
* Inject and use the service wherever you need it:
```c#
public class IbanValidatorController : ControllerBase
{
    private readonly IIbanValidator _ibanValidator;

    public IbanValidatorController(IIbanValidator ibanValidator)
    {
        _ibanValidator = ibanValidator;
    }

    ...
}
```
* Use the async or non-async *Validate* method from this service to validate the iban number (and bank account number).

## Repository example
In this repository you may find an ASP.NET Core Web Api project example named *IbanValidatorApi* to test the library. Launch the project and make a GET request (https or http). Examples:
* `https://localhost:5001/api/v1/IbanValidator/?iban=AD4621567359786953274469`
* `https://localhost:5001/api/v1/IbanValidator/?iban=PT50000101231234567890192&validateBban=true`

## License
MIT License
