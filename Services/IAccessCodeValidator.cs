using GatesApi.Models;

namespace GatesApi.Services;


public interface IAccessCodeValidator
{
    /// <summary>
    /// Проверяет код по всем правилам и возвращает результат со списком всех нарушений, а не только первым.
    /// </summary>
    AccessCodeValidationResult Validate(string accessCode);
}
