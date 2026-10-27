namespace GatesApi.Models;


public class ValidateAccessCodeRequest
{
    /// <summary>
    /// Код доступа для проверки.
    /// </summary>
    public string? AccessCode { get; set; }
}
