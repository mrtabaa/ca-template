using System.Text.RegularExpressions;

namespace Ca.Domain.Modules.Common.Validations;

internal static class ValidationExtensions
{
    internal static string? ValidateValue(
        this string? value,
        string variableName, int minLength = 0, int maxLength = 0, string? pattern = null
    )
    {
        if (string.IsNullOrWhiteSpace(value))
            return $"{variableName} is required.";

        if (minLength > 0 && value.Length < minLength)
            return $"{variableName} must be at least {minLength} characters.";

        if (maxLength > 0 && value.Length > maxLength)
            return $"{variableName} must be at most {maxLength} characters.";

        if (!string.IsNullOrWhiteSpace(pattern) && !Regex.IsMatch(value, pattern))
            return $"{variableName} format is invalid.";

        return null;
    }
}