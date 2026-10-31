# 🚗 UrbanDrive — Sistema de Gestão de Mobilidade Urbana (Grupo 3)

O **UrbanDrive** é um sistema de gestão para empresas de transporte multimodal desenvolvido em **C# (.NET 10.0)**, aplicando os conceitos fundamentais de Programação Orientada a Objetos (POO).

## 📂 Arquitetura do Projeto

O repositório segue a separação de responsabilidades recomendada na especificação do trabalho:

```text
sistema-gestao-mobilidade-urbana-gp3/
├── src/
│   ├── UrbanDrive.Models/
│   │   ├── Entities/
│   │   │   └── Veiculo.cs
│   │   ├── Interfaces/
│   │   ├── Enums/
│   │   └── UrbanDrive.Models.csproj
│   ├── UrbanDrive.Services/
│   │   └── UrbanDrive.Services.csproj
│   └── UrbanDrive.App/
│       ├── Program.cs
│       └── UrbanDrive.App.csproj
├── docs/
│   ├── diagramas/
│   ├── sprints/
│   └── README.md
├── tests/
├── .gitignore
├── UrbanDrive.slnx
└── README.md
```

## 🚀 Como Compilar e Executar

Na raiz do repositório, execute os comandos abaixo no terminal:

1. **Compilar toda a Solution:**
   ```bash
   dotnet build UrbanDrive.slnx
   ```

2. **Executar a aplicação de consola:**
   ```bash
   dotnet run --project src/UrbanDrive.App/UrbanDrive.App.csproj
   ```
