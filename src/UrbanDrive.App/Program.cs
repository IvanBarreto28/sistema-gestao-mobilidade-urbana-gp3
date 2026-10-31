using System.Globalization;
using UrbanDrive.Models.Entities;

namespace UrbanDrive.App;

class Program
{
    static void Main(string[] args)
    {
        ExecutarTestesDeValidacao();
        ExecutarMenu();
    }

    static void ExecutarMenu()
    {
        var veiculos = new List<Veiculo>();
        var proximoIdentificador = 1;
        var executando = true;

        while (executando)
        {
            ExibirMenu();
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            Console.Clear();

            switch (opcao)
            {
                case "1":
                    CadastrarVeiculo(veiculos, ref proximoIdentificador);
                    break;
                case "2":
                    ListarVeiculos(veiculos);
                    break;
                case "0":
                    executando = false;
                    Console.WriteLine("Programa encerrado.");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            if (executando)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("=== UrbanDrive ===");
        Console.WriteLine("1 - Cadastrar veículo");
        Console.WriteLine("2 - Listar veículos");
        Console.WriteLine("0 - Sair");
        Console.WriteLine();
    }

    static void CadastrarVeiculo(List<Veiculo> veiculos, ref int proximoIdentificador)
    {
        Console.WriteLine("=== Cadastrar veículo ===");
        Console.Write("Placa: ");
        var placa = Console.ReadLine() ?? string.Empty;

        Console.Write("Modelo: ");
        var modelo = Console.ReadLine() ?? string.Empty;

        Console.Write("Valor da diária: R$ ");
        var valorInformado = Console.ReadLine();

        if (!decimal.TryParse(valorInformado, NumberStyles.Number, CultureInfo.CurrentCulture, out var valorDiaria))
        {
            Console.WriteLine("Valor da diária inválido.");
            return;
        }

        try
        {
            var veiculo = new Veiculo(proximoIdentificador, placa, modelo, valorDiaria);
            veiculos.Add(veiculo);
            proximoIdentificador++;
            Console.WriteLine("Veículo cadastrado com sucesso.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Não foi possível cadastrar o veículo: {ex.Message}");
        }
    }

    static void ListarVeiculos(List<Veiculo> veiculos)
    {
        Console.WriteLine("=== Veículos cadastrados ===");

        if (veiculos.Count == 0)
        {
            Console.WriteLine("Nenhum veículo cadastrado.");
            return;
        }

        foreach (var veiculo in veiculos)
        {
            Console.WriteLine($"ID: {veiculo.Identificador}");
            Console.WriteLine($"Placa: {veiculo.Placa}");
            Console.WriteLine($"Modelo: {veiculo.Modelo}");
            Console.WriteLine($"Valor da diária: {veiculo.ValorDiaria:C}");
            Console.WriteLine(new string('-', 30));
        }
    }

    static void ExecutarTestesDeValidacao()
    {
        Console.WriteLine("=== UrbanDrive ===");

        var veiculo = new Veiculo(1, "ABC1D23", "Honda Civic", 150.00m);

        Console.WriteLine("Identificador: " + veiculo.Identificador);
        Console.WriteLine("Placa: " + veiculo.Placa);
        Console.WriteLine("Modelo: " + veiculo.Modelo);
        Console.WriteLine("Valor da diária: R$ " + veiculo.ValorDiaria.ToString("F2"));

        Console.WriteLine("\n=== Testes de Regras e Validações ===");

        try
        {
            Console.Write("[Teste 1 - Placa vazia]: ");
            var vPlacaVazia = new Veiculo(2, "", "Toyota Corolla", 160.00m);
            Console.WriteLine("FALHOU (Permitiu placa vazia)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("PASSOU -> " + ex.Message);
        }

        try
        {
            Console.Write("[Teste 2 - Placa nula]: ");
            var vPlacaNula = new Veiculo(3, null!, "Fiat Pulse", 130.00m);
            Console.WriteLine("FALHOU (Permitiu placa nula)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("PASSOU -> " + ex.Message);
        }

        try
        {
            Console.Write("[Teste 3 - Placa com espaços]: ");
            var vPlacaEspacos = new Veiculo(4, "   ", "Chevrolet Onix", 120.00m);
            Console.WriteLine("FALHOU (Permitiu placa com espaços)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("PASSOU -> " + ex.Message);
        }

        try
        {
            Console.Write("[Teste 4 - Alterar placa para vazio]: ");
            veiculo.Placa = "";
            Console.WriteLine("FALHOU (Permitiu alterar placa para vazio)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("PASSOU -> " + ex.Message);
        }

        try
        {
            Console.Write("[Teste 5 - Valor da diária negativo]: ");
            var vDiariaInvalida = new Veiculo(5, "XYZ9K88", "VW Polo", -50.00m);
            Console.WriteLine("FALHOU (Permitiu diária negativa)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("PASSOU -> " + ex.Message);
        }

        try
        {
            Console.Write("[Teste 6 - Identificador zero]: ");
            var vIdInvalido = new Veiculo(0, "DEF4G56", "Hyundai HB20", 110.00m);
            Console.WriteLine("FALHOU (Permitiu ID zero)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("PASSOU -> " + ex.Message);
        }

        Console.WriteLine();
    }
}
