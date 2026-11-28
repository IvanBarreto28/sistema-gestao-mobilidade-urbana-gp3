using System;
using System.Collections.Generic;
using System.Linq;
using UrbanDrive.Models.Entities;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Services
{
    /// <summary>
    /// Serviço responsável por coordenar frota, motoristas, locações e relatórios do UrbanDrive.
    /// Evita concentrar regras de negócio na camada de apresentação (Program.cs).
    /// </summary>
    public class GestaoMobilidadeService
    {
        private readonly Frota frota;
        private readonly List<Motorista> motoristas = new List<Motorista>();
        private readonly List<Locacao> locacoes = new List<Locacao>();
        private int proximoIdLocacao = 1;

        public Frota Frota => frota;
        public IReadOnlyList<Motorista> Motoristas => motoristas.AsReadOnly();
        public IReadOnlyList<Locacao> Locacoes => locacoes.AsReadOnly();

        public GestaoMobilidadeService(string nomeFrota = "Frota Principal UrbanDrive")
        {
            frota = new Frota(nomeFrota);
        }

        public void CadastrarVeiculo(Veiculo veiculo)
        {
            frota.AdicionarVeiculo(veiculo);
        }

        public void CadastrarMotorista(Motorista motorista)
        {
            if (motorista == null)
            {
                throw new ArgumentNullException(nameof(motorista), "O motorista não pode ser nulo.");
            }

            if (motoristas.Any(m => m.CPF == motorista.CPF))
            {
                throw new InvalidOperationException($"Já existe um motorista cadastrado com o CPF {motorista.CPF}.");
            }

            motoristas.Add(motorista);
        }

        public Locacao RegistrarLocacao(int idVeiculo, string cpfMotorista, int dias)
        {
            Veiculo? veiculo = frota.BuscarPorId(idVeiculo);
            if (veiculo == null)
            {
                throw new KeyNotFoundException($"Veículo com identificador {idVeiculo} não encontrado na frota.");
            }

            Motorista? motorista = motoristas.FirstOrDefault(m => m.CPF == cpfMotorista);
            if (motorista == null)
            {
                throw new KeyNotFoundException($"Motorista com CPF {cpfMotorista} não encontrado.");
            }

            Locacao locacao = new Locacao(proximoIdLocacao, veiculo, motorista, dias);
            proximoIdLocacao++;
            locacoes.Add(locacao);
            return locacao;
        }

        public IReadOnlyList<IImprimivel> ObterTodosImprimiveis()
        {
            List<IImprimivel> itens = new List<IImprimivel> { frota };
            itens.AddRange(frota.Veiculos);
            itens.AddRange(motoristas);
            itens.AddRange(locacoes);
            return itens.AsReadOnly();
        }
    }
}
