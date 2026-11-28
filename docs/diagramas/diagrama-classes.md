# Diagrama de Classes — UrbanDrive (após a parte de abstração e relatórios da Sprint 4)

> Para visualizar: abra este arquivo no GitHub (renderiza Mermaid automaticamente) ou cole o bloco em https://mermaid.live.
> Este diagrama registra a alteração de `Veiculo` para classe abstrata e a inclusão de `IImprimivel`.

```mermaid
classDiagram
    direction TB

    class Veiculo {
        <<abstract>>
        -int identificador
        -string placa
        -string modelo
        -decimal valorDiaria
        +int Identificador
        +string Placa
        +string Modelo
        +decimal ValorDiaria
        +Veiculo(int, string, string, decimal)
        +GerarRelatorio()* string
        +RealizarManutencao() string
        +RealizarManutencao(string) string
        +CalcularAluguel(int dias) decimal
    }
    note for Veiculo "CalcularAluguel é virtual. GerarRelatorio é abstrato."

    class Carro {
        +decimal SeguroDiario$
        -int quantidadePortas
        +int QuantidadePortas
        +Carro(int, string, string, decimal, int)
        +CalcularAluguel(int dias) decimal
        +GerarRelatorio() string
    }
    note for Carro "Relatório inclui a quantidade de portas."

    class Patinete {
        <<sealed>>
        +int DiasParaDesconto$
        +decimal PercentualDesconto$
        -int autonomiaBateria
        +int AutonomiaBateria
        +Patinete(int, string, string, decimal, int)
        +CalcularAluguel(int dias) decimal
        +GerarRelatorio() string
    }
    note for Patinete "Relatório inclui a autonomia da bateria."

    class IImprimivel {
        <<interface>>
        +GerarRelatorio() string
    }

    class Motorista {
        -string nome
        -string cpf
        +string Nome
        +string CPF
        +CNH CarteiraHabilitacao
        +Motorista(string, string, string, string, DateTime)
    }

    class CNH {
        -string numeroRegistro
        -string categoria
        -DateTime dataValidade
        +string NumeroRegistro
        +string Categoria
        +DateTime DataValidade
        ~CNH(string, string, DateTime)
    }

    class ConfiguradorSistema {
        <<static>>
        +decimal TaxaLocacaoPadrao$
        +decimal TaxaLocacao$
        +int TotalVeiculos$
        ~RegistrarVeiculo()$
        +AplicarTaxaLocacao(decimal) decimal
        +RestaurarTaxaPadrao()$
    }

    IImprimivel <|.. Veiculo : implementação
    Veiculo <|-- Carro : herança
    Veiculo <|-- Patinete : herança
    Motorista *-- "1" CNH : composição
    Veiculo ..> ConfiguradorSistema : registra no contador
```

## Legenda dos relacionamentos

| Relacionamento | Entre | Significado no código |
|---|---|---|
| Implementação | `Veiculo` → `IImprimivel` | `Veiculo` implementa a interface e declara `GerarRelatorio()` como abstrato. |
| Herança | `Carro` e `Patinete` → `Veiculo` | As classes concretas herdam os atributos e comportamentos comuns e implementam o relatório. |
| Composição | `Motorista` ◆— `CNH` | A `CNH` é criada dentro do construtor de `Motorista`. |
| Dependência | `Veiculo` ⇢ `ConfiguradorSistema` | O construtor de `Veiculo` registra a criação no contador. |

## Pendências do diagrama

- `Frota` ainda não está representada porque não existe no código desta versão.
- Não há relacionamento entre `Motorista` e `Veiculo` no código atual.
- A `TaxaLocacao` do `ConfiguradorSistema` ainda não é usada por `CalcularAluguel`.
