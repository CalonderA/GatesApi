using GatesApi.Models;

namespace GatesApi.Services;


public class AccessCodeValidator : IAccessCodeValidator
{
    private const int MinLength = 8;
    private const int MaxLength = 16;

    /// <summary>Символы</summary>
    private const string SpecialCharacters = "!@#$%";

    public AccessCodeValidationResult Validate(string accessCode)
    {
        var errors = new List<string>();

        if (accessCode.Length < MinLength || accessCode.Length > MaxLength)
        {
            errors.Add("Длина кода должна быть от 8 до 16 символов.");
        }

        if (!accessCode.Any(IsUpperCaseLetter))
        {
            errors.Add("Нет заглавной латинской буквы.");
        }

        if (!accessCode.Any(IsLowerCaseLetter))
        {
            errors.Add("Нет строчной латинской буквы.");
        }

        if (!accessCode.Any(IsDigit))
        {
            errors.Add("Нет цифры.");
        }

        if (!accessCode.Any(IsSpecialCharacter))
        {
            errors.Add("Нет специального символа.");
        }

        if (accessCode.Any(char.IsWhiteSpace))
        {
            errors.Add("Код не должен содержать пробелы.");
        }

        if (HasThreeIdenticalCharactersInRow(accessCode))
        {
            errors.Add("Обнаружены три или более одинаковых символа подряд.");
        }

        if (HasThreeOrMoreDigitsInRow(accessCode))
        {
            errors.Add("Обнаружена последовательность из трёх или более подряд идущих цифр.");
        }

        if (!IsLetter(accessCode[0]))
        {
            errors.Add("Первый символ должен быть буквой.");
        }

        char lastCharacter = accessCode[^1];
        if (!IsDigit(lastCharacter) && !IsSpecialCharacter(lastCharacter))
        {
            errors.Add("Последний символ должен быть цифрой или специальным символом.");
        }

        return new AccessCodeValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors,
            Message = errors.Count == 0 ? "Код принят. Ворота открываются." : "Код отклонён."
        };
    }

    

    private static bool IsUpperCaseLetter(char character) => character >= 'A' && character <= 'Z';

    private static bool IsLowerCaseLetter(char character) => character >= 'a' && character <= 'z';

    private static bool IsDigit(char character) => character >= '0' && character <= '9';

    private static bool IsLetter(char character) => IsUpperCaseLetter(character) || IsLowerCaseLetter(character);

    private static bool IsSpecialCharacter(char character) => SpecialCharacters.Contains(character);

    private static bool HasThreeIdenticalCharactersInRow(string code)
    {
        for (int i = 0; i <= code.Length - 3; i++)
        {
            if (code[i] == code[i + 1] && code[i] == code[i + 2])
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasThreeOrMoreDigitsInRow(string code)
    {
        int digitsInRow = 0;

        foreach (char character in code)
        {
            if (IsDigit(character))
            {
                digitsInRow++;

                if (digitsInRow >= 3)
                {
                    return true;
                }
            }
            else
            {
                digitsInRow = 0;
            }
        }

        return false;
    }
}
