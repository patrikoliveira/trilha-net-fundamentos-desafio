using DesafioFundamentos.Models.Validation;

namespace DesafioFundamentos.Models.ValueObject;

public class Preco
{
    private readonly decimal taxaEstacionamento;
    private readonly decimal precoHora;

    public Preco(decimal taxaEstacionamento, decimal precoHora)
    {
        this.taxaEstacionamento = taxaEstacionamento;
        this.precoHora = precoHora;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotLessThanOrEqualZero(TaxaEstacionamento, nameof(TaxaEstacionamento));
        DomainValidation.NotLessThanZero(PrecoHora, nameof(PrecoHora));
    }

    public decimal TaxaEstacionamento { get => taxaEstacionamento; }
    public decimal PrecoHora { get => precoHora; }
}
