using System.Text.RegularExpressions;
using DesafioFundamentos.Models.Exceptions;

namespace DesafioFundamentos.Models.Validation;

public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
        {
            throw new EntityValidationException($"{fieldName} não pode ser nulo");
        }
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
        {
            throw new EntityValidationException($"{fieldName} não pode ser vazio ou nulo");
        }
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target.Length < minLength)
        {
            throw new EntityValidationException($"{fieldName} deve ter pelo menos {minLength} caracteres");
        }
    }

    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
        {
            throw new EntityValidationException($"{fieldName} deve ser menor ou igual a {maxLength} caracteres");
        }
    }

    public static void IsValidLicensePlate(string target, string fieldName)
    {
        var pattern = @"^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$";
        if (!Regex.IsMatch(target, pattern))
        {
            throw new EntityValidationException($"{fieldName} não é válida, o formato deve ser ABC1234 ou ABC1C23");
        }
    }

    public static void NotLessThanOrEqualZero(decimal target, string fieldName)
    {
        if (target <= decimal.Zero)
        {
            throw new EntityValidationException($"{fieldName} não pode ser menor ou igual a zero");
        }
    }

    public static void NotLessThanZero(decimal target, string fieldName)
    {
        if (target < decimal.Zero)
        {
            throw new EntityValidationException($"{fieldName} não pode ser menor que zero");
        }
    }
}