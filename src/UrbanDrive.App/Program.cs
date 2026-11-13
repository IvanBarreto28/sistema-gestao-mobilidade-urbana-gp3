using System;
using UrbanDrive.Models.Entities;

namespace UrbanDrive.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== UrbanDrive ===");

            // 1. Caso de Sucesso
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

            // Teste 1: Placa vazia ("")
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

            // Teste 2: Placa nula (null)
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

            // Teste 3: Placa apenas com espaços em branco ("   ")
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

            // Teste 4: Tentativa de alterar a placa para vazio depois do objeto criado (Teste do Setter)
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

            // Teste 5: Valor da diária negativo ou zero
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

            // Teste 6: Identificador inválido (zero ou negativo)
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
        }

        //Membros estáticos (ConfiguradorSistema)
        private static void TestarConfiguradorSistema()
        {
            Console.WriteLine("\n=== Teste de Membros Estáticos (ConfiguradorSistema) ===");

            // Estado inicial: o Veiculo do caso de sucesso (Sprint 1) já foi contado
            Console.WriteLine("Taxa de locação inicial: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            Console.WriteLine("Veículos criados até agora: " + ConfiguradorSistema.TotalVeiculos);

            // Teste 7: o contador aumenta a cada veículo válido criado
            int antes = ConfiguradorSistema.TotalVeiculos;
            Veiculo v1 = new Veiculo(101, "AAA1A11", "Renault Kwid", 100.00m);
            Veiculo v2 = new Veiculo(102, "BBB2B22", "Fiat Argo", 200.00m);
            int depois = ConfiguradorSistema.TotalVeiculos;
            Console.WriteLine("[Teste 7 - Contador após criar 2 veículos]: " +
                (depois == antes + 2 ? "PASSOU" : "FALHOU") +
                " (antes: " + antes + ", depois: " + depois + ")");

            // Teste 8: veículo inválido NÃO deve ser contado
            antes = ConfiguradorSistema.TotalVeiculos;
            try
            {
                Veiculo invalido = new Veiculo(103, "", "Veículo Inválido", 100.00m);
            }
            catch (ArgumentException)
            {
                // esperado
            }
            Console.WriteLine("[Teste 8 - Veículo inválido não entra no contador]: " +
                (ConfiguradorSistema.TotalVeiculos == antes ? "PASSOU" : "FALHOU"));

            // Teste 9: dois objetos diferentes usam a MESMA taxa compartilhada
            Console.WriteLine("\n--- Acesso compartilhado entre objetos ---");
            ConfiguradorSistema.RestaurarTaxaPadrao();
            Console.WriteLine("Taxa em uso: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            Console.WriteLine(v1.Modelo + " -> diária R$ " + v1.ValorDiaria.ToString("F2") +
                " | com taxa R$ " + ConfiguradorSistema.AplicarTaxaLocacao(v1.ValorDiaria).ToString("F2"));
            Console.WriteLine(v2.Modelo + " -> diária R$ " + v2.ValorDiaria.ToString("F2") +
                " | com taxa R$ " + ConfiguradorSistema.AplicarTaxaLocacao(v2.ValorDiaria).ToString("F2"));

            // Teste 10: mudar a taxa uma vez afeta o cálculo para todos os objetos
            ConfiguradorSistema.TaxaLocacao = 0.25m;
            Console.WriteLine("\nTaxa alterada para: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            decimal novoV1 = ConfiguradorSistema.AplicarTaxaLocacao(v1.ValorDiaria);
            decimal novoV2 = ConfiguradorSistema.AplicarTaxaLocacao(v2.ValorDiaria);
            Console.WriteLine(v1.Modelo + " -> com taxa R$ " + novoV1.ToString("F2"));
            Console.WriteLine(v2.Modelo + " -> com taxa R$ " + novoV2.ToString("F2"));
            Console.WriteLine("[Teste 10 - Nova taxa vale para todos os objetos]: " +
                (novoV1 == 125.00m && novoV2 == 250.00m ? "PASSOU" : "FALHOU"));

            // Teste 11: taxa inválida é rejeitada e o valor anterior é mantido
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

            // Teste 12: taxa acima de 100% também é rejeitada
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

            // Teste 13: valor-base inválido no cálculo
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

            // Deixa o sistema no estado padrão para o resto do programa
            ConfiguradorSistema.RestaurarTaxaPadrao();
            Console.WriteLine("\nTaxa restaurada para o padrão: " + (ConfiguradorSistema.TaxaLocacao * 100).ToString("F0") + "%");
            Console.WriteLine("Total final de veículos criados: " + ConfiguradorSistema.TotalVeiculos);
        }

        // Sobrecarga de métodos (RealizarManutencao)
        private static void TestarManutencao(Veiculo veiculo)
        {
            Console.WriteLine("\n=== Teste de Manutenção de Veículos (Sobrecarga) ===");

            // Teste 14: RealizarManutencao() sem parâmetros (manutenção preventiva)
            string manutencaoPreventiva = veiculo.RealizarManutencao();
            Console.WriteLine("[Teste 14 - RealizarManutencao()]: " +
                (!string.IsNullOrWhiteSpace(manutencaoPreventiva) && manutencaoPreventiva.Contains("preventiva")
                    ? "PASSOU"
                    : "FALHOU") +
                " -> " + manutencaoPreventiva);

            // Teste 15: RealizarManutencao(string pecaSubstituida) com parâmetro (manutenção corretiva)
            string peca = "Pastilha de freio";
            string manutencaoCorretiva = veiculo.RealizarManutencao(peca);
            Console.WriteLine("[Teste 15 - RealizarManutencao(string pecaSubstituida)]: " +
                (manutencaoCorretiva.Contains("corretiva") && manutencaoCorretiva.Contains(peca)
                    ? "PASSOU"
                    : "FALHOU") +
                " -> " + manutencaoCorretiva);

            // Teste 16: garantir que as duas sobrecargas tenham comportamentos distintos
            Console.WriteLine("[Teste 16 - Comportamentos distintos entre sobrecargas]: " +
                (manutencaoPreventiva != manutencaoCorretiva ? "PASSOU" : "FALHOU"));

            // Teste 17: peça substituída inválida (espaços em branco)
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
