# Planejamento e Resultado — Sprint 3

## A. Identificação
- **Grupo:** Grupo 3
- **Integrantes:** Filipe, Guilherme Thiebaut, Ivan Barreto, Samuel Araújo Silva
- **Número da sprint:** 3
- **Período:** Semana 3 (Herança, polimorfismo e especialização)
- **Responsável pelo acompanhamento:** Guilherme Thiebaut

## B. Objetivo da sprint
Implementar a hierarquia de veículos (`Carro` e `Patinete` selada), aplicando herança com `base`, sobrescrita polimórfica de `CalcularAluguel(int dias)` e sobrecarga de `RealizarManutencao`.

## C. Itens selecionados do Product Backlog
| ID | Requisito ou item | Prioridade | Justificativa |
|---|---|---|---|
| PB06 | Especialização `Carro` e `Patinete` (`sealed`) + `CalcularAluguel` (RF02, RF05) | Alta | Demonstra herança, `sealed` e ligação dinâmica polimórfica. |
| PB07 | Sobrecarga `RealizarManutencao()` e `RealizarManutencao(string)` (RF06) | Média | Permite registrar manutenção preventiva e corretiva sem duplicar lógica. |

## D. Planejamento das tarefas
*(Unidade de esforço: Pontos de História — PH)*

| ID | Tarefa | Item relacionado | Responsável | Estimativa de esforço |
|---|---|---|---|---|
| T3.1 | Implementar `RealizarManutencao()` e `RealizarManutencao(string)` em `Veiculo` | PB07 | Guilherme Thiebaut | 2 PH |
| T3.2 | Implementar `Carro` herdando de `Veiculo` com `QuantidadePortas` e `SeguroDiario` | PB06 | Filipe | 3 PH |
| T3.3 | Implementar `Patinete` (`sealed`) herdando de `Veiculo` com desconto >= 3 dias | PB06 | Ivan Barreto | 3 PH |
| T3.4 | Implementar testes de polimorfismo (ligação dinâmica, `sealed`, sobrecarga) | PB06, PB07 | Samuel Araújo Silva | 3 PH |

## E. Critérios de aceitação
- **PB06:** Chamadas `CalcularAluguel(dias)` em referências do tipo `Veiculo` executam as regras específicas de `Carro` (+ R$ 15,00/dia) e `Patinete` (-10% a partir de 3 dias); `Patinete` é `sealed`.
- **PB07:** Ambas as sobrecargas funcionam para qualquer subtipo de `Veiculo`, rejeitando peça nula/vazia.

## F. Entregáveis previstos
- Classes `Carro.cs` e `Patinete.cs`;
- Sobrecargas de manutenção em `Veiculo.cs`;
- Testes 14 a 26 comprovando polimorfismo e herança.

## G. Resultado da sprint
| Item ou tarefa | Situação | Evidência | Observações |
|---|---|---|---|
| T3.1 — Sobrecarga de manutenção | Concluído | Commit `d0d6184` | Testes 14 a 17 validando preventiva e corretiva. |
| T3.2 — Classe `Carro` | Concluído | Commit `2beed1e` | Reaproveita validação de dias via `base.CalcularAluguel(dias)`. |
| T3.3 — Classe `Patinete` | Concluído | Commit `45e7368` | Marcada como `sealed` e com desconto progressivo. |
| T3.4 — Polimorfismo e documentação | Concluído | Commit `73d5ee2` | Testes 18 a 26 cobrindo ligação dinâmica e tipos. |

## H. Retrospectiva
- **O que funcionou bem?** O uso de `base` evitou duplicação da validação de `dias` nas subclasses.
- **Quais foram as principais dificuldades?** Garantir que `TestarPolimorfismo()` fosse integrado ao fluxo principal de execução sem conflitos de merge.
- **O planejamento foi adequado ao esforço necessário?** Sim, a divisão por classe/responsabilidade permitiu entregas paralelas limpas.
- **O que deverá ser melhorado na próxima sprint?** Conectar todos os métodos de teste ao `ExecutarTestes()` e concluir as pendências da Sprint 2 (`Frota` e `Locacao`).
