using System;
using System.Collections.Generic;
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

            // Teste de validação da autonomia da bateria
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

            TestarPolimorfismo();
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

        // Polimorfismo e integração (Sprint 3)
        private static void TestarPolimorfismo()
        {
            Console.WriteLine("\n=== Teste de Polimorfismo (referências do tipo Veiculo) ===");

            int totalAntes = ConfiguradorSistema.TotalVeiculos;

            // Três objetos de tipos diferentes, todos guardados como Veiculo
            List<Veiculo> frotaDemo = new List<Veiculo>
            {
                new Veiculo(201, "BAS1C01", "Veículo Genérico", 100.00m),
                new Carro(202, "CAR2R02", "Honda Civic", 150.00m, 4),
                new Patinete(203, "PAT3N03", "Xiaomi Mi Scooter", 35.00m, 45)
            };

            // Teste 18: integração com o ConfiguradorSistema (Carro e Patinete também são contados)
            int totalDepois = ConfiguradorSistema.TotalVeiculos;
            Console.WriteLine("[Teste 18 - Carro e Patinete entram no contador]: " +
                (totalDepois == totalAntes + 3 ? "PASSOU" : "FALHOU") +
                " (antes: " + totalAntes + ", depois: " + totalDepois + ")");

            // Teste 19: o mesmo comando (CalcularAluguel) em uma única lista, cada objeto responde do seu jeito
            int dias = 5;
            Console.WriteLine("\n--- Aluguel de " + dias + " dias, chamado por referência Veiculo ---");
            foreach (Veiculo v in frotaDemo)
            {
                decimal valor = v.CalcularAluguel(dias);
                Console.WriteLine(v.GetType().Name.PadRight(9) + " | " + v.Modelo.PadRight(18) +
                    " | diária R$ " + v.ValorDiaria.ToString("F2") + " | total R$ " + valor.ToString("F2"));
            }

            // Teste 20: ligação dinâmica (a chamada via Veiculo executa o override do tipo real)
            // Se Carro/Patinete tivessem escondido o método com "new" em vez de "override",
            // a chamada via Veiculo executaria a versão da classe-base e este teste falharia.
            Veiculo comoVeiculoCarro = frotaDemo[1];
            Carro comoCarro = (Carro)frotaDemo[1];
            Veiculo comoVeiculoPatinete = frotaDemo[2];
            Patinete comoPatinete = (Patinete)frotaDemo[2];
            Console.WriteLine("\n[Teste 20 - Ligação dinâmica (Carro)]: " +
                (comoVeiculoCarro.CalcularAluguel(dias) == comoCarro.CalcularAluguel(dias) ? "PASSOU" : "FALHOU"));
            Console.WriteLine("[Teste 20 - Ligação dinâmica (Patinete)]: " +
                (comoVeiculoPatinete.CalcularAluguel(dias) == comoPatinete.CalcularAluguel(dias) ? "PASSOU" : "FALHOU"));

            // Teste 21: valores esperados com as regras de cada tipo
            // Veiculo: diária x dias | Carro: diária x dias + seguro de R$ 15,00 por dia
            // Patinete: diária x dias, com 10% de desconto a partir de 3 dias
            // ATENÇÃO: se o grupo mudar alguma regra, atualizar estes valores.
            Console.WriteLine("[Teste 21 - Veiculo, 3 dias = R$ 300,00]: " +
                (frotaDemo[0].CalcularAluguel(3) == 300.00m ? "PASSOU" : "FALHOU"));
            Console.WriteLine("[Teste 21 - Carro, 5 dias = R$ 825,00]: " +
                (frotaDemo[1].CalcularAluguel(5) == 825.00m ? "PASSOU" : "FALHOU"));
            Console.WriteLine("[Teste 21 - Patinete, 5 dias = R$ 157,50]: " +
                (frotaDemo[2].CalcularAluguel(5) == 157.50m ? "PASSOU" : "FALHOU"));

            // Teste 21b: o desconto do Patinete só vale a partir de 3 dias
            Console.WriteLine("[Teste 21b - Patinete, 2 dias sem desconto = R$ 70,00]: " +
                (frotaDemo[2].CalcularAluguel(2) == 70.00m ? "PASSOU" : "FALHOU"));
            Console.WriteLine("[Teste 21b - Patinete, 3 dias com desconto = R$ 94,50]: " +
                (frotaDemo[2].CalcularAluguel(3) == 94.50m ? "PASSOU" : "FALHOU"));

            // Teste 21c: o mesmo número de dias gera valores diferentes por tipo (polimorfismo visível)
            decimal aluguelCarro = new Carro(204, "CAR4R04", "Fiat Argo", 100.00m, 4).CalcularAluguel(5);
            decimal aluguelPatinete = new Patinete(205, "PAT5N05", "Patinete Teste", 100.00m, 30).CalcularAluguel(5);
            Console.WriteLine("[Teste 21c - Mesma diária (R$ 100,00) e 5 dias: tipos diferentes dão valores diferentes]: " +
                (aluguelCarro == 575.00m && aluguelPatinete == 450.00m && aluguelCarro != aluguelPatinete ? "PASSOU" : "FALHOU") +
                " (Carro: R$ " + aluguelCarro.ToString("F2") + " | Patinete: R$ " + aluguelPatinete.ToString("F2") + ")");

            // Teste 22: a validação de dias vale para todos os tipos, mesmo via Veiculo
            foreach (Veiculo v in frotaDemo)
            {
                try
                {
                    Console.Write("[Teste 22 - " + v.GetType().Name + " com 0 dias]: ");
                    v.CalcularAluguel(0);
                    Console.WriteLine("FALHOU (Permitiu 0 dias)");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("PASSOU -> " + ex.Message);
                }
            }

            try
            {
                Console.Write("[Teste 22 - Carro com -3 dias]: ");
                frotaDemo[1].CalcularAluguel(-3);
                Console.WriteLine("FALHOU (Permitiu dias negativos)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }

            // Teste 23: uma referência Veiculo só enxerga o que Veiculo declara.
            // Para acessar membros específicos é preciso verificar o tipo real.
            Console.WriteLine("\n--- Membros específicos via verificação de tipo ---");
            foreach (Veiculo v in frotaDemo)
            {
                if (v is Carro c)
                {
                    Console.WriteLine(c.Modelo + " é um Carro com " + c.QuantidadePortas + " portas.");
                }
                else if (v is Patinete p)
                {
                    Console.WriteLine(p.Modelo + " é um Patinete com autonomia de " + p.AutonomiaBateria + " km.");
                }
                else
                {
                    Console.WriteLine(v.Modelo + " é um Veiculo genérico (sem membros específicos).");
                }
            }

            // Teste 24: Patinete é sealed (não pode ter subclasses)
            Console.WriteLine("\n[Teste 24 - Patinete é sealed]: " +
                (typeof(Patinete).IsSealed ? "PASSOU" : "FALHOU"));

            // Teste 25: Carro NÃO é sealed (CarroEletrico precisa herdar dele)
            Console.WriteLine("[Teste 25 - Carro não é sealed]: " +
                (!typeof(Carro).IsSealed ? "PASSOU" : "FALHOU"));

            // Teste 26: manutenção (sobrecarga) funciona em subclasses via herança
            Console.WriteLine("[Teste 26 - Manutenção herdada pelo Carro]: " +
                (comoCarro.RealizarManutencao().Contains("preventiva") &&
                 comoCarro.RealizarManutencao("Filtro de óleo").Contains("corretiva") ? "PASSOU" : "FALHOU"));
            Console.WriteLine("[Teste 26 - Manutenção herdada pelo Patinete]: " +
                (comoPatinete.RealizarManutencao().Contains("preventiva") &&
                 comoPatinete.RealizarManutencao("Pneu").Contains("corretiva") ? "PASSOU" : "FALHOU"));

            Console.WriteLine("\nTotal final de veículos criados: " + ConfiguradorSistema.TotalVeiculos);
        }
    }
}
