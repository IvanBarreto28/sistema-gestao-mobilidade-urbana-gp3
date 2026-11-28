# 📑 Relatório Final — UrbanDrive (Grupo 3)

## 1. Visão Geral
O **UrbanDrive** foi desenvolvido em C# (.NET 10.0) ao longo de **4 Sprints** com foco na aplicação prática dos pilares de Programação Orientada a Objetos (POO) e na gestão ágil do desenvolvimento em equipe.

## 2. Evolução por Sprints
- **Sprint 1 (Modelagem inicial e encapsulamento):** criação da solution (`UrbanDrive.slnx`) dividida em `UrbanDrive.Models`, `UrbanDrive.Services` e `UrbanDrive.App`; implementação da classe `Veiculo` com atributos privados, propriedades validadas e testes iniciais.
- **Sprint 2 (Relacionamentos e membros estáticos):** implementação da composição `Motorista` ↔ `CNH` (construtor `internal` em `CNH`) e de `ConfiguradorSistema` (`static`) com contador global de veículos e taxa de locação compartilhada. A classe `Frota` ficou pendente nesta sprint e foi retomada na Sprint 4.
- **Sprint 3 (Herança, polimorfismo e especialização):** criação de `Carro` e `Patinete` (`sealed`) herdando de `Veiculo`, uso de `base`, sobrescrita polimórfica de `CalcularAluguel(int dias)` e sobrecarga de `RealizarManutencao()`.
- **Sprint 4 (Abstração, interfaces e consolidação):** transformação de `Veiculo` em classe `abstract`, criação de `IImprimivel` e `IEletrico`, implementação de `CarroEletrico`, recuperação das pendências (`Frota` e `Locacao`), criação da camada `GestaoMobilidadeService` e integração completa no console.

## 3. Decisões Arquiteturais
1. **Separação de responsabilidades:** modelos e contratos em `UrbanDrive.Models`, regras de coordenação em `UrbanDrive.Services` (`GestaoMobilidadeService`) e interface com usuário em `UrbanDrive.App` (`Program.cs`).
2. **Composição estrita entre `Motorista` e `CNH`:** `CNH` possui construtor `internal` e é instanciada exclusivamente dentro de `Motorista`, impedindo sua existência isolada no domínio.
3. **Agregação em `Frota`:** `Frota` mantém uma coleção encapsulada (`IReadOnlyList<Veiculo>`) impedindo duplicidade de identificador ou placa, sem possuir exclusividade do ciclo de vida do veículo.
4. **Hierarquia elétrica:** `CarroEletrico` herda de `Carro` (reaproveitando portas e seguro diário) e implementa `IEletrico`, enquanto `Patinete` é `sealed` e também implementa `IEletrico`. `Carro` comum permanece não elétrico.
5. **Testabilidade não interativa:** adição da flag `--test` em `Program.cs` para execução determinística de toda a suíte de verificação em linha de comando.

## 4. Dificuldades Encontradas e Soluções
- **Quebra de compilação após abstração de `Veiculo`:** um teste legado em `Program.cs` ainda instanciava `new Veiculo(...)` diretamente; resolvido com `VeiculoGenericoTeste` (classe privada selada apenas para preservar testes legados).
- **Testes desacoplados do fluxo principal:** `TestarPolimorfismo`, `TestarRelatorios` e `TestarVeiculosEletricos` estavam definidos, mas não eram chamados em `ExecutarTestes()`. Todos foram integrados junto aos novos testes de `Frota` e `Locacao`.
- **Pendência da Sprint 2 (`Frota`):** registrada na retrospectiva da Sprint 2 e entregue na Sprint 4 junto com `Locacao` e `GestaoMobilidadeService`.

## 5. Evidência dos Testes Finais
Execução realizada via `dotnet build UrbanDrive.slnx` (0 avisos, 0 erros) e `dotnet run --project src/UrbanDrive.App/UrbanDrive.App.csproj --no-build -- --test`:
- **Encapsulamento e validações (`Veiculo`):** Testes 1 a 6 — **PASSOU**
- **Membros estáticos (`ConfiguradorSistema`):** Testes 7 a 13 — **PASSOU**
- **Sobrecarga de manutenção:** Testes 14 a 17 — **PASSOU**
- **Herança, `sealed` e polimorfismo (`CalcularAluguel`):** Testes 18 a 26 — **PASSOU**
- **Abstração e `IImprimivel`:** Testes de abstração e relatórios — **PASSOU**
- **Contratos elétricos (`IEletrico`, `Patinete`, `CarroEletrico`):** Testes 27 a 39 — **PASSOU**
- **Integração (`Frota`, `Locacao`, `GestaoMobilidadeService`):** Testes 40 a 45 — **PASSOU**
- **Resultado final:** `RESUMO DOS TESTES: TODOS OS TESTES PASSARAM` (código de saída `0`).
