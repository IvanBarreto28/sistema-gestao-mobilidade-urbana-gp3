# Planejamento e Resultado — Sprint 4

## A. Identificação
- **Grupo:** Grupo 3
- **Integrantes:** Filipe, Guilherme Thiebaut, Ivan Barreto, Samuel Araújo Silva
- **Número da sprint:** 4
- **Período:** Semana 4 (Abstração, interfaces e consolidação do sistema)
- **Responsável pelo acompanhamento:** Filipe

## B. Objetivo da sprint
Consolidar a arquitetura final do UrbanDrive tornando `Veiculo` abstrata, aplicando as interfaces `IImprimivel` e `IEletrico`, integrando os componentes das Sprints 1–3 (`Frota`, `Locacao`, `GestaoMobilidadeService`) e entregando a aplicação de console testada e documentada.

## C. Itens selecionados do Product Backlog
| ID | Requisito ou item | Prioridade | Justificativa |
|---|---|---|---|
| PB08 | `Veiculo` abstrata + interface `IImprimivel` (RF08) | Alta | Padroniza relatórios textuais entre todas as entidades. |
| PB09 | Interface `IEletrico`, `CarroEletrico` e `Patinete` elétrico (RF09) | Alta | Modela comportamento de bateria e recarga por contrato. |
| PB05 / PB10 | `Frota` (agregação) e `Locacao` (associação) + serviço `GestaoMobilidadeService` | Alta | Fecha a integração dos componentes pendentes e separa lógica do `Program.cs`. |
| PB11 / PB12 | Console integrado, testes finais (`--test`), diagrama e documentação final (RF10) | Alta | Garante entrega executável, verificável e bem documentada. |

## D. Planejamento das tarefas
*(Unidade de esforço: Pontos de História — PH)*

| ID | Tarefa | Item relacionado | Responsável | Estimativa de esforço |
|---|---|---|---|---|
| T4.1 | Tornar `Veiculo` abstrata e implementar `IImprimivel` nas entidades | PB08 | Filipe | 3 PH |
| T4.2 | Criar `IEletrico`, `CarroEletrico` e adaptar `Patinete` | PB09 | Samuel Araújo Silva | 4 PH |
| T4.3 | Implementar `Frota`, `Locacao`, `GestaoMobilidadeService` e menu completo no console | PB05, PB10, PB11 | Ivan Barreto / Guilherme Thiebaut | 4 PH |
| T4.4 | Executar testes finais, atualizar diagrama UML, backlog e relatório final | PB12 | Todos | 3 PH |

## E. Critérios de aceitação
- **PB08:** `typeof(Veiculo).IsAbstract == true`; `Carro`, `Patinete`, `CarroEletrico`, `Motorista`, `Frota` e `Locacao` implementam `IImprimivel`.
- **PB09:** `CarroEletrico` herda de `Carro` e implementa `IEletrico`; `Carro` comum não implementa `IEletrico`.
- **PB10 / PB11:** `dotnet build` sem erros/avisos; menu permite operar todas as funcionalidades; `dotnet run -- --test` aprova todos os 45+ testes.

## F. Entregáveis previstos
- Código-fonte integrado e executável;
- Diagrama de classes final (`docs/diagramas/diagrama-classes.md`);
- Product Backlog e documentos das 4 sprints;
- Relatório final e roteiro de apresentação.

## G. Resultado da sprint
| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T4.1 — `Veiculo` abstrata e `IImprimivel` | Concluído | Commit `a52dacb` + `Motorista`/`Frota`/`Locacao` | Todas as entidades principais geram relatório via `IImprimivel`. |
| T4.2 — `IEletrico` e `CarroEletrico` | Concluído | Commit `0de0552` / `07c7a92` | Testes 27 a 39 aprovados. |
| T4.3 — `Frota`, `Locacao` e `GestaoMobilidadeService` | Concluído | `Frota.cs`, `Locacao.cs`, `GestaoMobilidadeService.cs`, `Program.cs` | Corrigida instanciação legada de `Veiculo` abstrata e unificado o menu. |
| T4.4 — Testes finais e documentação | Concluído | `dotnet run -- --test` (45 testes) + `docs/` | Todos os testes passaram com 0 falhas. |

## H. Retrospectiva
- **O que funcionou bem?** A introdução de `GestaoMobilidadeService` limpou `Program.cs` e permitiu testar `Frota` e `Locacao` de ponta a ponta.
- **Quais foram as principais dificuldades?** Após `Veiculo` virar `abstract`, uma chamada legada em `Program.cs` quebrou a compilação e métodos de teste novos não estavam sendo chamados em `ExecutarTestes()`.
- **O planejamento foi adequado ao esforço necessário?** Sim, a sprint absorveu as pendências da Sprint 2 e consolidou todo o escopo.
- **O que deverá ser melhorado em projetos futuros?** Adotar testes automatizados em projeto dedicado desde a Sprint 1 e validar `dotnet build` em cada PR antes do merge.
