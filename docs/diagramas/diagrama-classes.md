# Diagrama de Classes — UrbanDrive (Versão Final)

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
        +GerarRelatorio() string
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

    class Frota {
        -string nome
        -List~Veiculo~ veiculos
        +string Nome
        +IReadOnlyList~Veiculo~ Veiculos
        +Frota(string)
        +AdicionarVeiculo(Veiculo)
        +RemoverVeiculo(int) bool
        +BuscarPorId(int) Veiculo
        +BuscarPorPlaca(string) Veiculo
        +ListarEletricos() IReadOnlyList~IEletrico~
        +CalcularValorTotalAluguel(int) decimal
        +GerarRelatorio() string
    }

    class Locacao {
        -int id
        -int dias
        +int Id
        +Veiculo Veiculo
        +Motorista Motorista
        +DateTime DataLocacao
        +int Dias
        +decimal TaxaAplicada
        +decimal ValorBase
        +decimal ValorTotal
        +Locacao(int, Veiculo, Motorista, int)
        +GerarRelatorio() string
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

    class GestaoMobilidadeService {
        -Frota frota
        -List~Motorista~ motoristas
        -List~Locacao~ locacoes
        -int proximoIdLocacao
        +Frota Frota
        +IReadOnlyList~Motorista~ Motoristas
        +IReadOnlyList~Locacao~ Locacoes
        +GestaoMobilidadeService(string)
        +CadastrarVeiculo(Veiculo)
        +CadastrarMotorista(Motorista)
        +RegistrarLocacao(int, string, int) Locacao
        +ObterTodosImprimiveis() IReadOnlyList~IImprimivel~
    }

    IImprimivel <|.. Veiculo : implementação
    IImprimivel <|.. Motorista : implementação
    IImprimivel <|.. Frota : implementação
    IImprimivel <|.. Locacao : implementação
    Veiculo <|-- Carro : herança
    Veiculo <|-- Patinete : herança
    Carro <|-- CarroEletrico : herança
    IEletrico <|.. Patinete : implementação
    IEletrico <|.. CarroEletrico : implementação
    Motorista *-- "1" CNH : composição
    Frota o-- "*" Veiculo : agregação
    Locacao --> "1" Veiculo : associação
    Locacao --> "1" Motorista : associação
    Locacao ..> ConfiguradorSistema : aplica taxa
    Veiculo ..> ConfiguradorSistema : registra no contador
    GestaoMobilidadeService --> "1" Frota : gerencia
    GestaoMobilidadeService --> "*" Motorista : gerencia
    GestaoMobilidadeService --> "*" Locacao : registra
```

## Legenda dos relacionamentos

| Relacionamento | Entre | Significado no código |
|---|---|---|
| Implementação | `Veiculo`, `Motorista`, `Frota`, `Locacao` → `IImprimivel` | Todas as entidades principais implementam `GerarRelatorio()`. |
| Implementação | `Patinete` e `CarroEletrico` → `IEletrico` | Implementam `NivelBateria`, `Rodar(int km)` e `Recarregar()`. |
| Herança | `CarroEletrico` → `Carro` → `Veiculo` | Especialização em dois níveis com reaproveitamento de atributos e regras. |
| Herança | `Patinete` (`sealed`) → `Veiculo` | Classe selada que impede subtipos e aplica desconto a partir de 3 dias. |
| Composição | `Motorista` ◆— `CNH` | A `CNH` tem construtor `internal` e só existe criada dentro do construtor de `Motorista`. |
| Agregação | `Frota` ◇— `Veiculo` | A frota agrupa veículos existentes sem destruir seu ciclo de vida independente. |
| Associação | `Locacao` → `Veiculo` e `Motorista` | Registra o aluguel vinculando um veículo a um motorista com CNH válida. |
| Dependência | `Veiculo` e `Locacao` ⇢ `ConfiguradorSistema` | Contador global de veículos e aplicação compartilhada da taxa de locação. |
| Serviço | `GestaoMobilidadeService` → domínio | Coordena operações de cadastro, locação e consolidação de relatórios fora de `Program.cs`. |
