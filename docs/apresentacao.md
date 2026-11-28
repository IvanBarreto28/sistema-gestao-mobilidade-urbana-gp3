# 🎤 Roteiro de Apresentação — UrbanDrive (Grupo 3)

## 1. Abertura e Contexto (1 min)
- Apresentar o problema: gestão de mobilidade urbana multimodal (carros, carros elétricos, patinetes, motoristas, frotas e locações).
- Mostrar a estrutura da solução (`UrbanDrive.Models`, `UrbanDrive.Services`, `UrbanDrive.App`, `docs/`).

## 2. Demonstração dos Conceitos de POO no Código (4 min)
1. **Encapsulamento e Validações (Sprint 1):** mostrar `Veiculo.cs` (campos privados, validações de placa, diária e ID).
2. **Composição, Agregação e Membros Estáticos (Sprint 2 + 4):**
   - `Motorista.cs` + `CNH.cs` (composição com construtor `internal`);
   - `Frota.cs` (agregação de `Veiculo` com proteção contra placa/ID duplicados);
   - `ConfiguradorSistema.cs` (`static`, contador global `TotalVeiculos` e `TaxaLocacao`).
3. **Herança, `sealed`, Sobrescrita e Sobrecarga (Sprint 3):**
   - `Carro.cs` (+ seguro diário) e `Patinete.cs` (`sealed`, desconto ≥ 3 dias);
   - `RealizarManutencao()` vs `RealizarManutencao(string pecaSubstituida)`.
4. **Abstração e Interfaces (Sprint 4):**
   - `Veiculo` como `abstract`;
   - `IImprimivel` implementada por `Veiculo`, `Motorista`, `Frota` e `Locacao`;
   - `IEletrico` implementada por `Patinete` e `CarroEletrico`.

## 3. Demonstração Prática ao Vivo (3 min)
1. Executar a suíte de testes automatizados:
   ```bash
   dotnet run --project src/UrbanDrive.App/UrbanDrive.App.csproj -- --test
   ```
2. Executar o menu interativo:
   ```bash
   dotnet run --project src/UrbanDrive.App/UrbanDrive.App.csproj
   ```
   - Cadastrar 1 `Carro`, 1 `Patinete` e 1 `CarroEletrico` (opção `1`);
   - Simular cálculo polimórfico para 5 dias (opção `3`);
   - Rodar e recarregar veículo elétrico via `IEletrico` (opção `4`);
   - Cadastrar motorista e registrar locação (opção `5`);
   - Exibir relatórios unificados via `IImprimivel` (opção `7`).

## 4. Encerramento (1 min)
- Mostrar o [Diagrama de Classes](diagramas/diagrama-classes.md), o [Product Backlog](product-backlog.md) e o [Relatório Final](relatorio-final.md).
- Destacar lições aprendidas: uso de camada de serviços (`GestaoMobilidadeService`) e validação contínua de compilação/testes.
