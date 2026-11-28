# 📋 Product Backlog — UrbanDrive (Grupo 3)

Backlog consolidado do sistema **UrbanDrive**, mapeando todos os requisitos funcionais mínimos (**RF01–RF10**) e sua evolução ao longo das **4 Sprints**.

## Histórico de Priorização e Status

| ID | Requisito | Item do Backlog | Prioridade | Sprint Alvo | Status |
|---|---|---|---|---|---|
| PB01 | Infraestrutura | Criar solução C# (`UrbanDrive.slnx`), projetos (`Models`, `Services`, `App`) e repositório GitHub | Alta | Sprint 1 | Concluído |
| PB02 | RF01 | Modelar e implementar `Veiculo` com encapsulamento, propriedades e validações (ID, placa, modelo, diária) | Alta | Sprint 1 | Concluído |
| PB03 | RF03 | Implementar `Motorista` e `CNH` com relacionamento de composição | Alta | Sprint 2 | Concluído |
| PB04 | RF07 | Implementar `ConfiguradorSistema` com membros estáticos (contador global e taxa de locação) | Alta | Sprint 2 | Concluído |
| PB05 | RF04 | Implementar `Frota` com agregação de `Veiculo` (adição, remoção, busca e cálculo consolidado) | Alta | Sprint 2 / 4 | Concluído |
| PB06 | RF02 / RF05 | Implementar `Carro` e `Patinete` (`sealed`) herdando de `Veiculo` e sobrescrevendo `CalcularAluguel(int)` | Alta | Sprint 3 | Concluído |
| PB07 | RF06 | Implementar sobrecarga `RealizarManutencao()` e `RealizarManutencao(string pecaSubstituida)` | Média | Sprint 3 | Concluído |
| PB08 | RF08 | Tornar `Veiculo` abstrata e criar interface `IImprimivel` aplicada às entidades | Alta | Sprint 4 | Concluído |
| PB09 | RF09 | Criar interface `IEletrico` (`NivelBateria`, `Rodar`, `Recarregar`) e implementar `CarroEletrico` e `Patinete` | Alta | Sprint 4 | Concluído |
| PB10 | RF05 | Implementar `Locacao` associando `Veiculo`, `Motorista` e `ConfiguradorSistema` (com validação de CNH) | Alta | Sprint 4 | Concluído |
| PB11 | RF10 | Criar camada `GestaoMobilidadeService` e integrar menu completo no console (`Program.cs`) | Alta | Sprint 4 | Concluído |
| PB12 | Qualidade | Consolidar suíte automatizada de testes (`--test`) e documentação final das sprints | Alta | Sprint 4 | Concluído |

## Rastreabilidade com Requisitos Funcionais

- **RF01 (Veículos):** atendido por `Veiculo.cs` (`PB02`).
- **RF02 (Especialização):** atendido por `Carro.cs`, `Patinete.cs` e `CarroEletrico.cs` (`PB06`, `PB09`).
- **RF03 (Motoristas e CNH):** atendido por `Motorista.cs` e `CNH.cs` (`PB03`).
- **RF04 (Frotas):** atendido por `Frota.cs` e `GestaoMobilidadeService.cs` (`PB05`, `PB11`).
- **RF05 (Cálculo de locação):** atendido por `CalcularAluguel(int)` polimórfico e `Locacao.cs` (`PB06`, `PB10`).
- **RF06 (Manutenção):** atendido por `RealizarManutencao()` / `RealizarManutencao(string)` (`PB07`).
- **RF07 (Configuração):** atendido por `ConfiguradorSistema.cs` (`PB04`).
- **RF08 (Relatórios):** atendido por `IImprimivel` em `Veiculo`, `Motorista`, `Frota` e `Locacao` (`PB08`).
- **RF09 (Recursos elétricos):** atendido por `IEletrico`, `Patinete` e `CarroEletrico` (`PB09`).
- **RF10 (Console):** atendido por `Program.cs` integrado a `GestaoMobilidadeService` (`PB11`).
