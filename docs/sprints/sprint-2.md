# Planejamento e Resultado — Sprint 2

## A. Identificação
- **Grupo:** Grupo 3
- **Integrantes:** Filipe, Guilherme Thiebaut, Ivan Barreto, Samuel Araújo Silva
- **Número da sprint:** 2
- **Período:** Semana 2 (Relacionamentos entre objetos e membros estáticos)
- **Responsável pelo acompanhamento:** Samuel Araújo Silva

## B. Objetivo da sprint
Ampliar o modelo de domínio do UrbanDrive para representar relacionamentos de composição (`Motorista` e `CNH`), agregação (`Frota` e `Veiculo`) e comportamento compartilhado por meio de membros estáticos (`ConfiguradorSistema`).

## C. Itens selecionados do Product Backlog
| ID | Requisito ou item | Prioridade | Justificativa |
|---|---|---|---|
| PB03 | `Motorista` e `CNH` com composição (RF03) | Alta | Garante que a CNH não exista de forma independente do motorista. |
| PB04 | `ConfiguradorSistema` com membros estáticos (RF07) | Alta | Centraliza contador global de veículos e taxa de locação compartilhada. |
| PB05 | `Frota` com agregação de `Veiculo` (RF04) | Alta | Necessário para agrupar e gerenciar coleções de veículos. |

## D. Planejamento das tarefas
*(Unidade de esforço: Pontos de História — PH)*

| ID | Tarefa | Item relacionado | Responsável | Estimativa de esforço |
|---|---|---|---|---|
| T2.1 | Implementar `CNH` com construtor `internal` e `Motorista` compondo `CNH` | PB03 | Filipe | 3 PH |
| T2.2 | Implementar `ConfiguradorSistema` estático e integrar contador no construtor de `Veiculo` | PB04 | Samuel Araújo Silva | 3 PH |
| T2.3 | Implementar classe `Frota` agregando coleção de `Veiculo` | PB05 | Ivan Barreto | 3 PH |
| T2.4 | Atualizar `Program.cs` com testes de `Motorista`/`CNH` e `ConfiguradorSistema` | PB03, PB04 | Guilherme Thiebaut | 2 PH |

## E. Critérios de aceitação
- **PB03:** `CNH` não pode ser instanciada livremente fora da composição de `Motorista`; campos vazios lançam `ArgumentException`.
- **PB04:** `ConfiguradorSistema.TotalVeiculos` incrementa apenas para veículos válidos; alteração em `TaxaLocacao` reflete imediatamente em todos os cálculos.
- **PB05:** `Frota` permite adicionar, remover e consultar veículos sem destruir as instâncias de `Veiculo`.

## F. Entregáveis previstos
- Classes `Motorista`, `CNH`, `ConfiguradorSistema` e `Frota`;
- Evidências de testes de composição e membros estáticos (Testes 7 a 13);
- Diagrama de classes atualizado.

## G. Resultado da sprint
| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T2.1 — `Motorista` e `CNH` | Concluído | Commit `082e027` | Composição garantida via construtor `internal` em `CNH`. |
| T2.2 — `ConfiguradorSistema` | Concluído | Commit `c644617` | Contador e taxa compartilhada funcionando; testes 7–13 aprovados. |
| T2.3 — Classe `Frota` | Não concluído | — | Adiada para consolidação na Sprint 4; inicialmente o console usou `List<Veiculo>`. |
| T2.4 — Testes no console | Concluído | `Program.cs` | Testes de `Motorista`, `CNH` e `ConfiguradorSistema` executados. |

## H. Retrospectiva
- **O que funcionou bem?** O uso de `internal` no construtor de `CNH` e `RegistrarVeiculo()` expressou com clareza os limites de encapsulamento.
- **Quais foram as principais dificuldades?** Sincronização de branches entre os membros gerou atraso na entrega da classe `Frota`.
- **O planejamento foi adequado ao esforço necessário?** Parcialmente; a integração via PRs consumiu mais tempo que o previsto, deixando `Frota` pendente.
- **O que deverá ser melhorado na próxima sprint?** Integrar `Frota` e `Locacao` antes da entrega final e revisar PRs com maior frequência.
