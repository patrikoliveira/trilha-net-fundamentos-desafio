using System.Globalization;
using DesafioFundamentos.Models.Validation;
using DesafioFundamentos.Models.ValueObject;

namespace DesafioFundamentos.Models;

public class Veiculo
{
    private PlacaVeicular placa;
    private string proprietario = string.Empty;
    private string modelo = string.Empty;
    private DateTime horaEntrada;
    private DateTime horaSaida;
    private TipoVeiculo tipo;
    private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");

    private Veiculo() { }

    public class Builder
    {
        private readonly Veiculo veiculo;

        public Builder()
        {
            veiculo = new Veiculo();
        }

        public Builder ComPlaca(string strPlaca)
        {
            var placa = new PlacaVeicular(strPlaca);
            veiculo.placa = placa;
            return this;
        }

        public Builder ComProprietario(string proprietario)
        {
            veiculo.proprietario = proprietario;
            return this;
        }

        public Builder ComModeloETipo(string modelo, TipoVeiculo tipo)
        {
            veiculo.modelo = modelo;
            veiculo.tipo = tipo;
            return this;
        }

        public Veiculo Build()
        {
            veiculo.Validate();
            return veiculo;
        }
    }

    private const int TAMANHO_MINIMO_PROPRIETARIO = 3;
    private const int TAMANHO_MAXIMO_PROPRIETARIO = 50;

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Proprietario, nameof(Proprietario));
        DomainValidation.MinLength(Proprietario, TAMANHO_MINIMO_PROPRIETARIO, nameof(Proprietario));
        DomainValidation.MaxLength(Proprietario, TAMANHO_MAXIMO_PROPRIETARIO, nameof(Proprietario));
    }

    public PlacaVeicular PlacaPolicial { get => placa; }
    public TipoVeiculo Tipo { get => tipo; }
    public string Proprietario { get => proprietario; }
    public string Modelo { get => modelo; }
    public DateTime HoraEntrada { get => horaEntrada; }
    public DateTime HoraSaida { get => horaSaida; }

    public void RegistrarHoraEntrada(DateTime? entrada = null)
    {
        horaEntrada = entrada ?? DateTime.Now;
    }

    public void RegistrarHoraSaida()
    {
        horaSaida = DateTime.Now;
    }

    public double TempoPermanencia {
        get => (HoraSaida - HoraEntrada).TotalHours;
    }

    public string ComprovanteEntrada()
    {
        return $"Veículo: {Modelo}, {PlacaPolicial} registrou entrada, {HoraEntrada.ToString("f", culture)}";
    }

    public override string ToString()
    {
        return $"Veículo: {Modelo}, {PlacaPolicial}, Proprietário: {Proprietario}, Entrada: {HoraEntrada.ToString("f", culture)}";
    }
}