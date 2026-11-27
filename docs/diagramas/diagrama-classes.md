# Diagrama de Classes — UrbanDrive (estado após a Sprint 3)

> Para visualizar: abra este arquivo no GitHub (renderiza Mermaid automaticamente) ou cole o bloco em https://mermaid.live.
> Reflete somente o que existe hoje no código. Classes das próximas sprints (Frota, CarroEletrico, interfaces) devem ser adicionadas quando forem integradas.

```mermaid
classDiagram
    direction TB

    class Veiculo {
        -int identificador
        -string placa
        -string modelo
        -decimal valorDiaria
        +int Identificador
        +string Placa
        +string Modelo
        +decimal ValorDiaria
        +Veiculo(int identificador, string placa, string modelo, decimal valorDiaria)
        +RealizarManutencao() string
        +RealizarManutencao(string pecaSubstituida) string
        +CalcularAluguel(int dias) decimal
    }
    note for Veiculo "CalcularAluguel é virtual (base do polimorfismo).\nRealizarManutencao tem duas sobrecargas."

    class Carro {
        +decimal SeguroDiario$
        -int quantidadePortas
        +int QuantidadePortas
        +Carro(int, string, string, decimal, int quantidadePortas)
        +CalcularAluguel(int dias) decimal
    }
    note for Carro "override: base + seguro de R$ 15,00 por dia"

    class Patinete {
        <<sealed>>
        +int DiasParaDesconto$
        +decimal PercentualDesconto$
        -int autonomiaBateria
        +int AutonomiaBateria
        +Patinete(int, string, string, decimal, int autonomiaBateria)
        +CalcularAluguel(int dias) decimal
    }
    note for Patinete "classe sealed: não admite subclasses.\noverride: base com 10% de desconto a partir de 3 dias"

    class Motorista {
        -string nome
        -string cpf
        +string Nome
        +string CPF
        +CNH CarteiraHabilitacao
        +Motorista(string nome, string cpf, string numeroRegistro, string categoria, DateTime dataValidade)
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
    note for CNH "Construtor internal: só pode ser criada\npor dentro do projeto Models (via Motorista)."

    class ConfiguradorSistema {
        <<static>>
        +decimal TaxaLocacaoPadrao$
        +decimal TaxaLocacao$
        +int TotalVeiculos$
        ~RegistrarVeiculo()$
        +AplicarTaxaLocacao(decimal valorBase)$ decimal
        +RestaurarTaxaPadrao()$
    }

    Veiculo <|-- Carro : herança
    Veiculo <|-- Patinete : herança
    Motorista *-- "1" CNH : composição
    Veiculo ..> ConfiguradorSistema : registra no contador
```

## Legenda dos relacionamentos

| Relacionamento | Entre | Significado no código |
|---|---|---|
| Herança | `Carro` → `Veiculo`, `Patinete` → `Veiculo` | `: base(...)` no construtor e `override` em `CalcularAluguel`, que chama `base.CalcularAluguel` |
| Composição | `Motorista` ◆— `CNH` | A `CNH` é criada dentro do construtor de `Motorista`; não existe sem ele |
| Dependência | `Veiculo` ⇢ `ConfiguradorSistema` | O construtor de `Veiculo` chama `RegistrarVeiculo()` após validar os dados |

## Pendências do diagrama
- `Frota` (agregação com `Veiculo`) ainda não está na main. Adicionar quando for integrada.
- Não há relacionamento entre `Motorista` e `Veiculo` no código atual.
- A `TaxaLocacao` do `ConfiguradorSistema` ainda não é usada por `CalcularAluguel`.
