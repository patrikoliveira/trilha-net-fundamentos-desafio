namespace DesafioFundamentos.Models.Exceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException(string? message) : base(message) { }
}

