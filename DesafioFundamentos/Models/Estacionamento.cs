using System.Globalization;
using DesafioFundamentos.Models.ValueObject;

namespace DesafioFundamentos.Models;

public class Estacionamento
{
    private readonly Preco preco;
    private readonly List<Veiculo> veiculos;
    private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");

    public Estacionamento(decimal taxaEstacionamento, decimal precoHora)
    {
        preco = new Preco(taxaEstacionamento, precoHora);
        veiculos = new List<Veiculo>();
    }

    public Preco Preco { get => preco; }

    public static string LerDado(string msn)
    {
        Console.WriteLine(msn);
        var dado = Console.ReadLine();
        return String.IsNullOrWhiteSpace(dado) ? "" : dado;
    }

    public static Veiculo LerDadosVeiculo()
    {
        var placa = LerDado("Digite a placa do veículo para estacionar:");
        var modelo = LerDado("Digite o modelo do veículo para estacionar:");
        var proprietario = LerDado("Digite o proprietário do veículo:");
        int tipo = Convert.ToInt32(LerDado("Digite o tipo do veículo: 0 - Carro, 1 - Moto"));

        var veiculo = new Veiculo.Builder()
            .ComPlaca(placa)
            .ComProprietario(proprietario)
            .ComModeloETipo(modelo, (TipoVeiculo)tipo)
            .Build();

        return veiculo;
    }

    public void AdicionarVeiculo()
    {
        var veiculo = LerDadosVeiculo();

        var veiculoJaEstacionado = veiculos.FirstOrDefault(x => x.PlacaPolicial.Placa.ToUpper() == veiculo.PlacaPolicial.Placa.ToUpper());
        if (veiculoJaEstacionado != null)
        {
            Console.WriteLine($"Veículo de {veiculo.PlacaPolicial.Placa} já está estacionado, Confira se digitou a placa corretamente");
            return;
        }

        veiculo.RegistrarHoraEntrada();
        veiculos.Add(veiculo);

        Console.WriteLine($"Estacionado com sucesso!\n{veiculo.ComprovanteEntrada()}");
    }

    public void RemoverVeiculo()
    {
        string placa = LerDado("Digite a placa do veículo para remover:");
        
        var veiculo = veiculos.FirstOrDefault(x => x.PlacaPolicial.Placa.ToUpper() == placa.ToUpper());

        // Verifica se o veículo existe
        if (veiculo != null)
        {
            veiculo.RegistrarHoraSaida();

            double horas = veiculo.TempoPermanencia;
            Console.WriteLine(horas);
            decimal valorTotal = preco.TaxaEstacionamento + preco.PrecoHora * (decimal)horas;

            veiculos.Remove(veiculo);

            Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: {valorTotal.ToString("C2", culture)}");
        }
        else
        {
            Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
        }
    }

    public void ListarVeiculos()
    {
        // Verifica se há veículos no estacionamento
        if (veiculos.Any())
        {
            Console.WriteLine("Os veículos estacionados são:");
            foreach (var veiculo in veiculos)
            {
                Console.WriteLine(veiculo);
            }
        }
        else
        {
            Console.WriteLine("Não há veículos estacionados.");
        }
    }
}
