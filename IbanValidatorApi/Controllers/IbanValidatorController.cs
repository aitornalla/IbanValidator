using IbanValidator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IbanValidatorApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IbanValidatorController : ControllerBase
    {
        private readonly IIbanValidator _ibanValidator;
        private readonly ILogger<IbanValidatorController> _logger;

        public IbanValidatorController(IIbanValidator ibanValidator, ILogger<IbanValidatorController> logger)
        {
            _ibanValidator = ibanValidator;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Validate([FromQuery] string iban, [FromQuery] bool validateBban = false)
        {
            return Ok(await _ibanValidator.ValidateAsync(iban, validateBban));
        }
    }
}
