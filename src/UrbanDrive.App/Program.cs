using System;
using System.Collections.Generic;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== UrbanDrive ===");

            // 1. Caso de Sucesso
            Veiculo veiculo = new VeiculoGenericoTeste(
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
                Veiculo vPlacaVazia = new VeiculoGenericoTeste(2, "", "Toyota Corolla", 160.00m);
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
                Veiculo vPlacaNula = new VeiculoGenericoTeste(3, null!, "Fiat Pulse", 130.00m);
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
                Veiculo vPlacaEspacos = new VeiculoGenericoTeste(4, "   ", "Chevrolet Onix", 120.00m);
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
                Veiculo vDiariaInvalida = new VeiculoGenericoTeste(5, "XYZ9K88", "VW Polo", -50.00m);
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
                Veiculo vIdInvalido = new VeiculoGenericoTeste(0, "DEF4G56", "Hyundai HB20", 110.00m);
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
            TestarRelatorios();
            TestarVeiculosEletricos();
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
            Veiculo v1 = new VeiculoGenericoTeste(101, "AAA1A11", "Renault Kwid", 100.00m);
            Veiculo v2 = new VeiculoGenericoTeste(102, "BBB2B22", "Fiat Argo", 200.00m);
            int depois = ConfiguradorSistema.TotalVeiculos;
            Console.WriteLine("[Teste 7 - Contador após criar 2 veículos]: " +
                (depois == antes + 2 ? "PASSOU" : "FALHOU") +
                " (antes: " + antes + ", depois: " + depois + ")");

            // Teste 8: veículo inválido NÃO deve ser contado
            antes = ConfiguradorSistema.TotalVeiculos;
            try
            {
                Veiculo invalido = new VeiculoGenericoTeste(103, "", "Veículo Inválido", 100.00m);
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
                new VeiculoGenericoTeste(201, "BAS1C01", "Veículo Genérico", 100.00m),
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

        // Testes da interface IImprimivel e da classe abstrata Veiculo (Sprint 4)
        private static void TestarRelatorios()
        {
            Console.WriteLine("\n=== Teste da Interface IImprimivel ===");

            Console.WriteLine("[Teste - Veiculo é abstrata]: " +
                (typeof(Veiculo).IsAbstract ? "PASSOU" : "FALHOU"));

            List<IImprimivel> itensImprimiveis = new List<IImprimivel>
            {
                new Carro(301, "REL3C01", "Toyota Corolla", 180.00m, 4),
                new Patinete(302, "REL3P02", "Segway Ninebot", 40.00m, 35)
            };

            foreach (IImprimivel item in itensImprimiveis)
            {
                string relatorio = item.GerarRelatorio();
                Console.WriteLine(relatorio);

                bool relatorioValido = !string.IsNullOrWhiteSpace(relatorio);
                bool contemDetalheEspecifico =
                    item is Carro ? relatorio.Contains("Portas:") :
                    item is Patinete && relatorio.Contains("Autonomia:");

                Console.WriteLine("[Teste - Relatório e detalhe específico]: " +
                    (relatorioValido && contemDetalheEspecifico ? "PASSOU" : "FALHOU"));
            }
        }

        // Veículos elétricos: IEletrico, Patinete e CarroEletrico (Sprint 4)
        private static void TestarVeiculosEletricos()
        {
            Console.WriteLine("\n=== Teste de Veículos Elétricos (IEletrico) ===");

            // Teste 27: contratos e hierarquia
            Resultado("Teste 27 - Patinete implementa IEletrico",
                typeof(IEletrico).IsAssignableFrom(typeof(Patinete)));
            Resultado("Teste 27 - CarroEletrico implementa IEletrico",
                typeof(IEletrico).IsAssignableFrom(typeof(CarroEletrico)));
            Resultado("Teste 27 - CarroEletrico herda de Carro",
                typeof(Carro).IsAssignableFrom(typeof(CarroEletrico)));
            Resultado("Teste 27 - Carro comum NÃO é elétrico",
                !typeof(IEletrico).IsAssignableFrom(typeof(Carro)));

            // Teste 28: criação e integração com o contador do ConfiguradorSistema
            int totalAntes = ConfiguradorSistema.TotalVeiculos;
            Patinete patinete = new Patinete(401, "ELE1P01", "Patinete Elétrico X", 40.00m, 40);
            CarroEletrico carroEletrico = new CarroEletrico(402, "ELE2C02", "BYD Dolphin", 200.00m, 4, 60.0m, 400);
            Resultado("Teste 28 - Patinete e CarroEletrico entram no contador (+2)",
                ConfiguradorSistema.TotalVeiculos == totalAntes + 2);

            // Teste 29: bateria começa cheia
            Resultado("Teste 29 - Patinete começa com 100% de bateria", patinete.NivelBateria == 100);
            Resultado("Teste 29 - CarroEletrico começa com 100% de bateria", carroEletrico.NivelBateria == 100);

            // Teste 30: Rodar consome a bateria proporcionalmente à autonomia
            // Patinete: 10 km de 40 km = 25% | CarroEletrico: 100 km de 400 km = 25%
            patinete.Rodar(10);
            carroEletrico.Rodar(100);
            Resultado("Teste 30 - Patinete rodou 10 km (autonomia 40 km) e ficou com 75%", patinete.NivelBateria == 75);
            Resultado("Teste 30 - CarroEletrico rodou 100 km (autonomia 400 km) e ficou com 75%", carroEletrico.NivelBateria == 75);

            // Teste 31: Recarregar volta a 100% e cada tipo descreve a recarga do seu jeito
            string msgPatinete = patinete.Recarregar();
            string msgCarro = carroEletrico.Recarregar();
            Console.WriteLine(msgPatinete);
            Console.WriteLine(msgCarro);
            Resultado("Teste 31 - Patinete recarregado volta a 100% e informa +25%",
                patinete.NivelBateria == 100 && msgPatinete.Contains("+25%"));
            string energiaEsperada = (60.0m * 25 / 100m).ToString("F1") + " kWh";
            Resultado("Teste 31 - CarroEletrico recarregado volta a 100% e informa a energia em kWh",
                carroEletrico.NivelBateria == 100 && msgCarro.Contains("+25%") && msgCarro.Contains(energiaEsperada));

            // Teste 32: recarregar com a bateria cheia não altera nada
            string msgCheia = patinete.Recarregar();
            Resultado("Teste 32 - Recarregar com bateria cheia não altera o nível",
                patinete.NivelBateria == 100 && msgCheia.Contains("já está"));

            // Teste 33: bateria insuficiente (41 km exigem 103% de um patinete de 40 km de autonomia)
            ExigirExcecao<InvalidOperationException>("Teste 33 - Patinete: 41 km sem bateria suficiente",
                () => patinete.Rodar(41));
            Resultado("Teste 33 - Nível não muda depois da tentativa que falhou", patinete.NivelBateria == 100);
            ExigirExcecao<InvalidOperationException>("Teste 33 - CarroEletrico: 401 km sem bateria suficiente",
                () => carroEletrico.Rodar(401));

            // Teste 34: distância inválida
            ExigirExcecao<ArgumentException>("Teste 34 - Patinete: 0 km", () => patinete.Rodar(0));
            ExigirExcecao<ArgumentException>("Teste 34 - CarroEletrico: -10 km", () => carroEletrico.Rodar(-10));

            // Teste 35: polimorfismo pela interface IEletrico (mesma chamada, comportamentos diferentes)
            Console.WriteLine("\n--- Polimorfismo por IEletrico ---");
            List<IEletrico> eletricos = new List<IEletrico> { patinete, carroEletrico };
            foreach (IEletrico e in eletricos)
            {
                e.Rodar(20);
                Console.WriteLine(e.GetType().Name + " rodou 20 km -> bateria em " + e.NivelBateria + "%");
            }
            Resultado("Teste 35 - Mesmo Rodar(20) consome valores diferentes (Patinete 50%, CarroEletrico 95%)",
                patinete.NivelBateria == 50 && carroEletrico.NivelBateria == 95);

            List<string> mensagens = new List<string>();
            foreach (IEletrico e in eletricos)
            {
                string msg = e.Recarregar();
                mensagens.Add(msg);
                Console.WriteLine(msg);
            }
            Resultado("Teste 35 - Recarregar pela interface deixa os dois em 100% com mensagens diferentes",
                patinete.NivelBateria == 100 && carroEletrico.NivelBateria == 100 && mensagens[0] != mensagens[1]);

            // Teste 36: validações do construtor de CarroEletrico
            ExigirExcecao<ArgumentException>("Teste 36 - CarroEletrico com capacidade 0 kWh",
                () => new CarroEletrico(403, "ELE3C03", "Modelo Inválido", 150.00m, 4, 0m, 300));
            ExigirExcecao<ArgumentException>("Teste 36 - CarroEletrico com autonomia 0 km",
                () => new CarroEletrico(404, "ELE4C04", "Modelo Inválido", 150.00m, 4, 50.0m, 0));
            ExigirExcecao<ArgumentException>("Teste 36 - CarroEletrico com 0 portas (validação herdada de Carro)",
                () => new CarroEletrico(405, "ELE5C05", "Modelo Inválido", 150.00m, 0, 50.0m, 300));

            // Teste 37: cálculo de aluguel do CarroEletrico por polimorfismo
            // Diária R$ 200,00 por 5 dias: base 1000,00 + seguro 75,00 + taxa de recarga 50,00 = 1125,00
            Veiculo comoVeiculo = carroEletrico;
            Carro carroComum = new Carro(406, "ELE6C06", "Carro Comum", 200.00m, 4);
            Resultado("Teste 37 - CarroEletrico, R$ 200,00 x 5 dias = R$ 1125,00 (via Veiculo)",
                comoVeiculo.CalcularAluguel(5) == 1125.00m);
            Resultado("Teste 37 - Carro comum com a mesma diária e dias = R$ 1075,00",
                carroComum.CalcularAluguel(5) == 1075.00m);
            ExigirExcecao<ArgumentException>("Teste 37 - CarroEletrico com 0 dias (via Veiculo)",
                () => comoVeiculo.CalcularAluguel(0));

            // Teste 38: relatórios (IImprimivel) incluem os dados da bateria
            IImprimivel imprimivelCarro = carroEletrico;
            IImprimivel imprimivelPatinete = patinete;
            string relCarro = imprimivelCarro.GerarRelatorio();
            string relPatinete = imprimivelPatinete.GerarRelatorio();
            Console.WriteLine("\n" + relCarro);
            Console.WriteLine(relPatinete);
            Resultado("Teste 38 - Relatório do CarroEletrico traz portas, kWh, autonomia e bateria",
                relCarro.Contains("CARRO ELÉTRICO") && relCarro.Contains("Portas:") &&
                relCarro.Contains("kWh") && relCarro.Contains("Autonomia:") && relCarro.Contains("Bateria: 100%"));
            Resultado("Teste 38 - Relatório do Patinete traz autonomia e bateria",
                relPatinete.Contains("Autonomia:") && relPatinete.Contains("Bateria: 100%"));

            // Teste 39: manutenção herdada pelo CarroEletrico
            Resultado("Teste 39 - Manutenção herdada pelo CarroEletrico",
                carroEletrico.RealizarManutencao().Contains("preventiva") &&
                carroEletrico.RealizarManutencao("Freio").Contains("corretiva"));

            // Demonstração: lista mista de Veiculo, separando os elétricos com verificação de tipo
            Console.WriteLine("\n--- Lista mista de Veiculo (aluguel de 5 dias) ---");
            List<Veiculo> mista = new List<Veiculo> { carroComum, carroEletrico, patinete };
            foreach (Veiculo v in mista)
            {
                string tipo = v is IEletrico eletrico
                    ? "elétrico, bateria em " + eletrico.NivelBateria + "%"
                    : "não elétrico";
                Console.WriteLine(v.GetType().Name.PadRight(14) + " | " + v.Modelo.PadRight(20) +
                    " | aluguel R$ " + v.CalcularAluguel(5).ToString("F2").PadLeft(8) + " | " + tipo);
            }
        }

        private static void Resultado(string nome, bool passou)
        {
            Console.WriteLine("[" + nome + "]: " + (passou ? "PASSOU" : "FALHOU"));
        }

        // Executa a ação e espera uma exceção do tipo T (ou de um tipo derivado)
        private static void ExigirExcecao<T>(string nome, Action acao) where T : Exception
        {
            Console.Write("[" + nome + "]: ");
            try
            {
                acao();
                Console.WriteLine("FALHOU (nenhuma exceção foi lançada)");
            }
            catch (T ex)
            {
                Console.WriteLine("PASSOU -> " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FALHOU (lançou " + ex.GetType().Name + " em vez do tipo esperado)");
            }
        }

        // Classe concreta usada somente para preservar os testes legados
        // que simulavam um veículo genérico antes de Veiculo se tornar abstrata.
        private sealed class VeiculoGenericoTeste : Veiculo
        {
            public VeiculoGenericoTeste(int identificador, string placa, string modelo, decimal valorDiaria)
                : base(identificador, placa, modelo, valorDiaria)
            {
            }

            public override string GerarRelatorio()
            {
                return $"[VEÍCULO DE TESTE] ID: {Identificador} | Placa: {Placa} | " +
                       $"Modelo: {Modelo} | Diária: R$ {ValorDiaria:F2}";
            }
        }

    }
}
