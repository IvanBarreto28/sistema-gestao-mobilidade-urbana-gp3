# Diagrama de Classes — UrbanDrive

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
        -int nivelBateria
        +int AutonomiaBateria
        +int NivelBateria
        +Patinete(int, string, string, decimal, int)
        +Rodar(int km)
        +Recarregar() string
        +CalcularAluguel(int dias) decimal
        +GerarRelatorio() string
    }
    note for Patinete "Recarga rápida na base, sempre até 100%.\nRelatório inclui autonomia e nível da bateria."

    class CarroEletrico {
        +decimal TaxaRecargaDiaria$
        -decimal capacidadeBateriaKwh
        -int autonomiaBateria
        -int nivelBateria
        +decimal CapacidadeBateriaKwh
        +int AutonomiaBateria
        +int NivelBateria
        +CarroEletrico(int, string, string, decimal, int, decimal, int)
        +Rodar(int km)
        +Recarregar() string
        +CalcularAluguel(int dias) decimal
        +GerarRelatorio() string
    }
    note for CarroEletrico "Recarga em estação, informando a energia em kWh.\nAluguel: Carro + taxa de recarga por dia."

    class IEletrico {
        <<interface>>
        +int NivelBateria
        +Rodar(int km)
        +Recarregar() string
    }

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
    Carro <|-- CarroEletrico : herança
    IEletrico <|.. Patinete : implementação
    IEletrico <|.. CarroEletrico : implementação
    Motorista *-- "1" CNH : composição
    Veiculo ..> ConfiguradorSistema : registra no contador
```

## Legenda dos relacionamentos

| Relacionamento | Entre | Significado no código |
|---|---|---|
| Implementação | `Veiculo` → `IImprimivel` | `Veiculo` implementa a interface e declara `GerarRelatorio()` como abstrato. |
| Implementação | `Patinete` e `CarroEletrico` → `IEletrico` | Os dois implementam `NivelBateria`, `Rodar(int km)` e `Recarregar()`, cada um do seu jeito. |
| Herança | `CarroEletrico` → `Carro` | Reaproveita portas e seguro; soma a taxa de recarga ao aluguel e acrescenta os dados da bateria ao relatório. |
| Herança | `Carro` e `Patinete` → `Veiculo` | As classes concretas herdam os atributos e comportamentos comuns e implementam o relatório. |
| Composição | `Motorista` ◆— `CNH` | A `CNH` é criada dentro do construtor de `Motorista`. |
| Dependência | `Veiculo` ⇢ `ConfiguradorSistema` | O construtor de `Veiculo` registra a criação no contador. |