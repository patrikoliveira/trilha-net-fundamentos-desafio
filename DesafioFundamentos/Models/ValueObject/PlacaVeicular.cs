using DesafioFundamentos.Models.Validation;

namespace DesafioFundamentos.Models.ValueObject;

public class PlacaVeicular
{
    private readonly string placa;
    public string Placa { get => placa; }

    public PlacaVeicular(string placa)
    {
        this.placa = placa;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.IsValidLicensePlate(Placa, nameof(Placa));
    }

    public override string ToString()
    {
        return $"Placa: {placa}";
    }
}