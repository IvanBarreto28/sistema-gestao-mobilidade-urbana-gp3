


using System;
using System.Collections.Generic;
using UrbanDrive.Models.Entities;

namespace UrbanDrive.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Veiculo> veiculos = new List<Veiculo>();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("       URBANDRIVE - MENU PRINCIPAL  ");
                Console.WriteLine("====================================");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Consultar veículos cadastrados");
                Console.WriteLine("3 - Demonstrar cálculos polimórficos");
                Console.WriteLine("4 - Executar testes das classes");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha uma opção: ");

                string entrada = Console.ReadLine() ?? "";
                if (!int.TryParse(entrada, out opcao))
                {
                    Console.WriteLine("\nOpção inválida. Digite um número.");
                    Pausar();
                    opcao = -1;
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        CadastrarVeiculo(veiculos);
                        break;
                    case 2:
                        ConsultarVeiculos(veiculos);
                        break;
                    case 3:
                        DemonstrarCalculoPolimorfico(veiculos);
                        break;
                    case 4:
                        Console.Clear();
                        ExecutarTestes();
                        Pausar();
                        break;
                    case 0:
                        Console.WriteLine("\nEncerrando o UrbanDrive...");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida.");
                        Pausar();
                        break;
                }
            }
        }

        private static void CadastrarVeiculo(List<Veiculo> veiculos)
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRO DE VEÍCULO ===");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Patinete");
            Console.Write("\nTipo de veículo: ");

            string tipo = Console.ReadLine() ?? "";

            if (tipo != "1" && tipo != "2")
            {
                Console.WriteLine("\nTipo inválido.");
                Pausar();
                return;
            }

            try
            {
                Console.Write("Identificador (número inteiro > 0): ");
                int id = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Placa: ");
                string placa = Console.ReadLine() ?? "";

                Console.Write("Modelo: ");
                string modelo = Console.ReadLine() ?? "";

                Console.Write("Valor da diária (R$): ");
                decimal valorDiaria = decimal.Parse(Console.ReadLine() ?? "0");

                if (tipo == "1")
                {
                    Console.Write("Quantidade de portas: ");
                    int portas = int.Parse(Console.ReadLine() ?? "0");

                    Carro carro = new Carro(id, placa, modelo, valorDiaria, portas);
                    veiculos.Add(carro);
                    Console.WriteLine("\nCarro cadastrado com sucesso!");
                }
                else
                {
                    Console.Write("Autonomia da bateria (km): ");
                    int autonomia = int.Parse(Console.ReadLine() ?? "0");

                    Patinete patinete = new Patinete(id, placa, modelo, valorDiaria, autonomia);
                    veiculos.Add(patinete);
                    Console.WriteLine("\nPatinete cadastrado com sucesso!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nErro: Formato numérico inválido. Verifique os valores digitados.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nErro de validação: " + ex.Message);
            }

            Pausar();
        }

        private static void ConsultarVeiculos(List<Veiculo> veiculos)
        {
            Console.Clear();
            Console.WriteLine("=== VEÍCULOS CADASTRADOS ===\n");

            if (veiculos.Count == 0)
            {
                Console.WriteLine("Nenhum veículo cadastrado.");
                Pausar();
                return;
            }

            for (int i = 0; i < veiculos.Count; i++)
            {
                Veiculo v = veiculos[i];

                Console.WriteLine($"ID: {v.Identificador} | Placa: {v.Placa} | Modelo: {v.Modelo}");
                Console.WriteLine($"Valor da Diária: R$ {v.ValorDiaria:F2}");

                if (v is Carro carro)
                {
                    Console.WriteLine($"Tipo: Carro | Portas: {carro.QuantidadePortas}");
                }
                else if (v is Patinete patinete)
                {
                    Console.WriteLine($"Tipo: Patinete | Autonomia da Bateria: {patinete.AutonomiaBateria} km");
                }

                Console.WriteLine("------------------------------------");
            }

            Pausar();
        }

        private static void DemonstrarCalculoPolimorfico(List<Veiculo> veiculos)
        {
            Console.Clear();
            Console.WriteLine("=== CÁLCULO POLIMÓRFICO DE ALUGUEL ===\n");

            if (veiculos.Count == 0)
            {
                Console.WriteLine("Cadastre ao menos um veículo antes de realizar o cálculo.");
                Pausar();
                return;
            }

            try
            {
                Console.Write("Informe a quantidade de dias para simular o aluguel: ");
                int dias = int.Parse(Console.ReadLine() ?? "0");

                Console.WriteLine($"\nResultado para {dias} dia(s):\n");

                for (int i = 0; i < veiculos.Count; i++)
                {
                    Veiculo v = veiculos[i];
                    decimal total = v.CalcularAluguel(dias);

                    Console.WriteLine($"[{v.GetType().Name}] {v.Modelo} (Placa: {v.Placa}) -> Total: R$ {total:F2}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nErro: Digite um número inteiro válido para os dias.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nErro de validação: " + ex.Message);
            }

            Pausar();
        }

        private static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        private static void ExecutarTestes()
        {
            Console.WriteLine("=== UrbanDrive ===");

            Veiculo veiculo = new Veiculo(
                1,
                "ABC1D23",
                "Honda Civic",
                150.00m
            );

            Console.WriteLine("Identificador: " + veiculo.Identificador);
            Console.WriteLine("Placa: " + veiculo.Placa);
            Console.WriteLine("Modelo: " + veiculo.Modelo);
            Console.WriteLine("Valor da diária: R$ " + veiculo.ValorDiaria.ToString("F2"));

            Console.WriteLine("\n=== Testes de Regras e Validações ===");

            try
            {
                Console.Write("[Teste 1 - Placa vazia]: ");
                Veiculo vPlacaVazia = new Veiculo(2, "", "Toyota Corolla", 160.00m);
                Console.WriteLine("FALHOU (Permitiu placa vazia)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            try
            {
                Console.Write("[Teste 2 - Placa nula]: ");
                Veiculo vPlacaNula = new Veiculo(3, null!, "Fiat Pulse", 130.00m);
                Console.WriteLine("FALHOU (Permitiu placa nula)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            try
            {
                Console.Write("[Teste 3 - Placa com espaços]: ");
                Veiculo vPlacaEspacos = new Veiculo(4, "   ", "Chevrolet Onix", 120.00m);
                Console.WriteLine("FALHOU (Permitiu placa com espaços)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            try
            {
                Console.Write("[Teste 4 - Alterar Placa via set para vazio]: ");
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
                Veiculo vDiariaInvalida = new Veiculo(5, "XYZ9K88", "VW Polo", -50.00m);
                Console.WriteLine("FALHOU (Permitiu diária negativa)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            try
            {
                Console.Write("[Teste 6 - Identificador zero]: ");
                Veiculo vIdInvalido = new Veiculo(0, "DEF4G56", "Hyundai HB20", 110.00m);
                Console.WriteLine("FALHOU (Permitiu ID zero)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            Console.WriteLine("\n=== Teste de Motorista e CNH ===");

            Motorista motorista = new Motorista(
                "Filipe",
                "123.456.789-00",
                "123456789",
                "AB",
                new DateTime(2030, 12, 31)
            );

            Console.WriteLine("Nome: " + motorista.Nome);
            Console.WriteLine("CPF: " + motorista.CPF);

            Console.WriteLine("\n=== Dados da CNH ===");

            Console.WriteLine("Registro: " + motorista.CarteiraHabilitacao.NumeroRegistro);
            Console.WriteLine("Categoria: " + motorista.CarteiraHabilitacao.Categoria);
            Console.WriteLine("Validade: " + motorista.CarteiraHabilitacao.DataValidade.ToShortDateString());

            TestarConfiguradorSistema();
            TestarManutencao(veiculo);

            Console.WriteLine("\n=== Teste da Classe Carro ===");

            Carro carro = new Carro(
                10,
                "ABC1D23",
                "Honda Civic",
                150.00m,
                4
            );

            Console.WriteLine("Modelo: " + carro.Modelo);
            Console.WriteLine("Placa: " + carro.Placa);
            Console.WriteLine("Quantidade de portas: " + carro.QuantidadePortas);
            Console.WriteLine("Valor da diária: R$ " + carro.ValorDiaria.ToString("F2"));

            decimal valorAluguel = carro.CalcularAluguel(5);
            Console.WriteLine("Aluguel por 5 dias: R$ " + valorAluguel.ToString("F2"));

            Console.WriteLine("\n=== Teste da Classe Patinete ===");

            Patinete patinete = new Patinete(
                11,
                "PAT1E23",
                "Xiaomi Mi Scooter",
                35.00m,
                45
            );

            Console.WriteLine("Modelo: " + patinete.Modelo);
            Console.WriteLine("Placa: " + patinete.Placa);
            Console.WriteLine("Autonomia da bateria: " + patinete.AutonomiaBateria + " km");
            Console.WriteLine("Valor da diária: R$ " + patinete.ValorDiaria.ToString("F2"));

            decimal valorAluguelPatinete = patinete.CalcularAluguel(5);
            Console.WriteLine("Aluguel por 5 dias: R$ " + valorAluguelPatinete.ToString("F2"));

            try
            {
                Console.Write("[Teste Patinete - Autonomia inválida]: ");
                Patinete pInvalido = new Patinete(12, "PAT9Z99", "Patinete Inválido", 30.00m, 0);
                Console.WriteLine("FALHOU (Permitiu autonomia zero ou negativa)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }
        }

        private static void TestarConfiguradorSistema()
        {
            Console.WriteLine("\n=== Teste de Membros Estáticos (ConfiguradorSistema) ===");

            Console.WriteLine("Taxa de locação inicial: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            Console.WriteLine("Veículos criados até agora: " + ConfiguradorSistema.TotalVeiculos);

            int antes = ConfiguradorSistema.TotalVeiculos;
            Veiculo v1 = new Veiculo(101, "AAA1A11", "Renault Kwid", 100.00m);
            Veiculo v2 = new Veiculo(102, "BBB2B22", "Fiat Argo", 200.00m);
            int depois = ConfiguradorSistema.TotalVeiculos;
            Console.WriteLine("[Teste 7 - Contador após criar 2 veículos]: " +
                (depois == antes + 2 ? "PASSOU" : "FALHOU") +
                " (antes: " + antes + ", depois: " + depois + ")");

            antes = ConfiguradorSistema.TotalVeiculos;
            try
            {
                Veiculo invalido = new Veiculo(103, "", "Veículo Inválido", 100.00m);
            }
            catch (ArgumentException)
            {
            }
            Console.WriteLine("[Teste 8 - Veículo inválido não entra no contador]: " +
                (ConfiguradorSistema.TotalVeiculos == antes ? "PASSOU" : "FALHOU"));

            Console.WriteLine("\n--- Acesso compartilhado entre objetos ---");
            ConfiguradorSistema.RestaurarTaxaPadrao();
            Console.WriteLine("Taxa em uso: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            Console.WriteLine(v1.Modelo + " -> diária R$ " + v1.ValorDiaria.ToString("F2") +
                " | com taxa R$ " + ConfiguradorSistema.AplicarTaxaLocacao(v1.ValorDiaria).ToString("F2"));
            Console.WriteLine(v2.Modelo + " -> diária R$ " + v2.ValorDiaria.ToString("F2") +
                " | com taxa R$ " + ConfiguradorSistema.AplicarTaxaLocacao(v2.ValorDiaria).ToString("F2"));

            ConfiguradorSistema.TaxaLocacao = 0.25m;
            Console.WriteLine("\nTaxa alterada para: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            decimal novoV1 = ConfiguradorSistema.AplicarTaxaLocacao(v1.ValorDiaria);
            decimal novoV2 = ConfiguradorSistema.AplicarTaxaLocacao(v2.ValorDiaria);
            Console.WriteLine(v1.Modelo + " -> com taxa R$ " + novoV1.ToString("F2"));
            Console.WriteLine(v2.Modelo + " -> com taxa R$ " + novoV2.ToString("F2"));
            Console.WriteLine("[Teste 10 - Nova taxa vale para todos os objetos]: " +
                (novoV1 == 125.00m && novoV2 == 250.00m ? "PASSOU" : "FALHOU"));

            try
            {
                Console.Write("[Teste 11 - Taxa negativa]: ");
                ConfiguradorSistema.TaxaLocacao = -0.05m;
                Console.WriteLine("FALHOU (Permitiu taxa negativa)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            try
            {
                Console.Write("[Teste 12 - Taxa acima de 100%]: ");
                ConfiguradorSistema.TaxaLocacao = 1.5m;
                Console.WriteLine("FALHOU (Permitiu taxa acima de 100%)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            try
            {
                Console.Write("[Teste 13 - Valor-base zero no cálculo]: ");
                ConfiguradorSistema.AplicarTaxaLocacao(0m);
                Console.WriteLine("FALHOU (Permitiu valor-base zero)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            ConfiguradorSistema.RestaurarTaxaPadrao();
            Console.WriteLine("\nTaxa restaurada para o padrão: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            Console.WriteLine("Total final de veículos criados: " + ConfiguradorSistema.TotalVeiculos);
        }

        private static void TestarManutencao(Veiculo veiculo)
        {
            Console.WriteLine("\n=== Teste de Manutenção de Veículos (Sobrecarga) ===");

            string manutencaoPreventiva = veiculo.RealizarManutencao();
            Console.WriteLine("[Teste 14 - RealizarManutencao()]: " +
                (!string.IsNullOrWhiteSpace(manutencaoPreventiva) && manutencaoPreventiva.Contains("preventiva")
                    ? "PASSOU"
                    : "FALHOU") +
                " -> " + manutencaoPreventiva);

            string peca = "Pastilha de freio";
            string manutencaoCorretiva = veiculo.RealizarManutencao(peca);
            Console.WriteLine("[Teste 15 - RealizarManutencao(string pecaSubstituida)]: " +
                (manutencaoCorretiva.Contains("corretiva") && manutencaoCorretiva.Contains(peca)
                    ? "PASSOU"
                    : "FALHOU") +
                " -> " + manutencaoCorretiva);

            Console.WriteLine("[Teste 16 - Comportamentos distintos entre sobrecargas]: " +
                (manutencaoPreventiva != manutencaoCorretiva ? "PASSOU" : "FALHOU"));

            try
            {
                Console.Write("[Teste 17 - Peça substituída com espaços]: ");
                veiculo.RealizarManutencao("   ");
                Console.WriteLine("FALHOU (Permitiu peça com espaços)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }
        }
    }
}