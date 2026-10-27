using GatesApi.Models;
using GatesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GatesApi.Controllers;

[ApiController]
[Route("api/gates")]
public class GatesController : ControllerBase
{
    private readonly IAccessCodeValidator _validator;

    public GatesController(IAccessCodeValidator validator)
    {
        _validator = validator;
    }


    [HttpPost("validate")]
    [ProducesResponseType(typeof(AccessCodeValidationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AccessCodeValidationResult), StatusCodes.Status400BadRequest)]
    public IActionResult Validate([FromBody] ValidateAccessCodeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AccessCode))
        {
            return BadRequest(new AccessCodeValidationResult
            {
                IsValid = false,
                Errors = new List<string> { "Код доступа не указан или пуст." },
                Message = "Некорректный запрос."
            });
        }

        AccessCodeValidationResult result = _validator.Validate(request.AccessCode);
        return Ok(result);
    }
}
