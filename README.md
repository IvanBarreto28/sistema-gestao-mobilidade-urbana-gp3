# 🚗 UrbanDrive — Sistema de Gestão de Mobilidade Urbana (Grupo 3)

O **UrbanDrive** é um sistema de gerenciamento para empresas de transporte multimodal desenvolvido em **C# (.NET 10.0)**, aplicando os conceitos fundamentais de Programação Orientada a Objetos (POO): encapsulamento, composição, agregação, associação, membros estáticos, herança, polimorfismo, classes abstratas/seladas e interfaces.

## 👥 Integrantes do Grupo 3
- Filipe
- Guilherme Thiebaut
- Ivan Barreto
- Samuel Araújo Silva

## 📂 Arquitetura do Projeto

```text
sistema-gestao-mobilidade-urbana-gp3/
├── src/
│   ├── UrbanDrive.Models/
│   │   ├── Entities/          # Veiculo, Carro, CarroEletrico, Patinete, Motorista, CNH, Frota, Locacao, ConfiguradorSistema
│   │   └── Interfaces/        # IImprimivel, IEletrico
│   ├── UrbanDrive.Services/
│   │   └── GestaoMobilidadeService.cs
│   └── UrbanDrive.App/
│       └── Program.cs         # Aplicação de console e suíte de testes (--test)
├── docs/
│   ├── diagramas/
│   │   └── diagrama-classes.md
│   ├── sprints/
│   │   ├── sprint-1.md
│   │   ├── sprint-2.md
│   │   ├── sprint-3.md
│   │   └── sprint-4.md
│   ├── product-backlog.md
│   ├── relatorio-final.md
│   ├── apresentacao.md
│   └── README.md
├── UrbanDrive.slnx
└── README.md
```

## 🛠️ Pré-requisitos e Instalação

1. Instale o **.NET 10.0 SDK** (verificado com `dotnet --version` >= `10.0`).
2. Clone o repositório:
   ```bash
   git clone https://github.com/IvanBarreto28/sistema-gestao-mobilidade-urbana-gp3.git
   cd sistema-gestao-mobilidade-urbana-gp3
   ```

## 🚀 Como Compilar, Executar e Testar

1. **Compilar toda a Solution:**
   ```bash
   dotnet build UrbanDrive.slnx
   ```

2. **Executar a aplicação de console (modo interativo):**
   ```bash
   dotnet run --project src/UrbanDrive.App/UrbanDrive.App.csproj
   ```
   O menu oferece operações para:
   - Cadastrar veículos (`Carro`, `Patinete`, `CarroEletrico`);
   - Consultar veículos e contador global (`ConfiguradorSistema`);
   - Simular cálculos polimórficos de aluguel;
   - Operar veículos elétricos (`Rodar` e `Recarregar` via `IEletrico`);
   - Cadastrar motorista (`Motorista` + `CNH`) e registrar `Locacao`;
   - Realizar manutenção preventiva e corretiva (sobrecarga);
   - Exibir relatórios unificados via `IImprimivel`;
   - Executar a suíte de testes integrada.

3. **Executar a suíte automatizada de testes (modo não interativo):**
   ```bash
   dotnet run --project src/UrbanDrive.App/UrbanDrive.App.csproj -- --test
   ```
   Executa os 45+ cenários de verificação (encapsulamento, composição, agregação, membros estáticos, herança, polimorfismo, `IImprimivel`, `IEletrico`, `Frota` e `Locacao`) e retorna código `0` se todos passarem.
