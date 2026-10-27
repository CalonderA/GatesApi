namespace GatesApi.Models;

public class AccessCodeValidationResult
{
    /// <summary>
    /// Пройдены ли все проверки.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Список всех обнаруженных нарушений.
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Итоговое сообщение.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
