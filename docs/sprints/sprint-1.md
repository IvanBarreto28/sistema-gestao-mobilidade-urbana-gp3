# Planejamento e Resultado — Sprint 1

## A. Identificação
- **Grupo:** Grupo 3
- **Integrantes:** Filipe, Guilherme Thiebaut, Ivan Barreto, Samuel Araújo Silva
- **Número da sprint:** 1
- **Período:** Semana 1 (Modelagem inicial e encapsulamento)
- **Responsável pelo acompanhamento:** Ivan Barreto

## B. Objetivo da sprint
Estabelecer a arquitetura inicial do repositório C# e entregar a primeira versão executável do UrbanDrive capaz de instanciar `Veiculo` aplicando encapsulamento e validações de entrada.

## C. Itens selecionados do Product Backlog
| ID | Requisito ou item | Prioridade | Justificativa |
|---|---|---|---|
| PB01 | Estrutura da solução (`UrbanDrive.slnx`, `Models`, `Services`, `App`) e GitHub | Alta | Base obrigatória para organização modular e trabalho paralelo em branches. |
| PB02 | Implementação inicial da classe `Veiculo` (RF01) | Alta | Entidade central da qual todas as demais categorias dependem. |

## D. Planejamento das tarefas
*(Unidade de esforço: Pontos de História — PH)*

| ID | Tarefa | Item relacionado | Responsável | Estimativa de esforço |
|---|---|---|---|---|
| T1.1 | Criar solution `UrbanDrive.slnx` e projetos `.csproj` + `.gitignore` | PB01 | Ivan Barreto | 2 PH |
| T1.2 | Modelar e implementar `Veiculo` com campos privados, propriedades e construtor | PB02 | Guilherme Thiebaut | 3 PH |
| T1.3 | Implementar validações (ID > 0, placa não vazia/nula, modelo, diária > 0) | PB02 | Samuel Araújo Silva | 2 PH |
| T1.4 | Criar testes iniciais no console demonstrando instanciação e exceções | PB02 | Filipe | 2 PH |

## E. Critérios de aceitação
- **PB01:** Solution compila via `dotnet build` com separação entre `UrbanDrive.Models`, `UrbanDrive.Services` e `UrbanDrive.App`.
- **PB02:** Não é possível criar ou alterar `Veiculo` com placa nula, vazia ou apenas espaços, ID ≤ 0 ou diária ≤ 0; testes no console comprovam o lançamento de `ArgumentException`.

## F. Entregáveis previstos
- Repositório configurado com estrutura modular;
- Classe `Veiculo.cs` encapsulada;
- Programa de console executável com testes 1 a 6 de validação.

## G. Resultado da sprint
| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T1.1 — Estrutura da solução | Concluído | Commit `f98bfd0` | Projetos criados em `src/` para .NET 10.0. |
| T1.2 — Classe `Veiculo` | Concluído | `src/UrbanDrive.Models/Entities/Veiculo.cs` | Campos privados e propriedades públicas implementados. |
| T1.3 — Validações de dados | Concluído | `Veiculo.cs` (`ArgumentException`) | Normalização de placa com `Trim().ToUpper()`. |
| T1.4 — Testes iniciais | Concluído | `Program.cs` (Testes 1 a 6) | Execução validando cenários positivos e negativos. |

## H. Retrospectiva
- **O que funcionou bem?** A divisão clara de pastas (`Models`, `Services`, `App`) facilitou a compilação e evitou conflitos iniciais.
- **Quais foram as principais dificuldades?** Alinhamento inicial do fluxo de upload/commits no GitHub entre os integrantes.
- **O planejamento foi adequado ao esforço necessário?** Sim, todas as tarefas previstas foram concluídas dentro da sprint.
- **O que deverá ser melhorado na próxima sprint?** Adotar branches por funcionalidade e mensagens estritamente no padrão Conventional Commits.
