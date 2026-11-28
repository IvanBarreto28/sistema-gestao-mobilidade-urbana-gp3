using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities
{
    /// <summary>
    /// Representa uma frota corporativa que agrega veículos administrados pelo sistema (RF04).
    /// </summary>
    public class Frota : IImprimivel
    {
        private string nome = string.Empty;
        private readonly List<Veiculo> veiculos = new List<Veiculo>();

        public string Nome
        {
            get { return nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O nome da frota não pode ser nulo ou vazio.");
                }
                nome = value.Trim();
            }
        }

        public IReadOnlyList<Veiculo> Veiculos => veiculos.AsReadOnly();

        public Frota(string nome)
        {
            Nome = nome;
        }

        public void AdicionarVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
            {
                throw new ArgumentNullException(nameof(veiculo), "O veículo não pode ser nulo.");
            }

            if (veiculos.Any(v => v.Identificador == veiculo.Identificador))
            {
                throw new InvalidOperationException($"Já existe um veículo com o identificador {veiculo.Identificador} na frota.");
            }

            if (veiculos.Any(v => v.Placa.Equals(veiculo.Placa, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Já existe um veículo com a placa {veiculo.Placa} na frota.");
            }

            veiculos.Add(veiculo);
        }

        public bool RemoverVeiculo(int identificador)
        {
            Veiculo? veiculo = BuscarPorId(identificador);
            if (veiculo == null)
            {
                return false;
            }

            return veiculos.Remove(veiculo);
        }

        public Veiculo? BuscarPorId(int identificador)
        {
            return veiculos.FirstOrDefault(v => v.Identificador == identificador);
        }

        public Veiculo? BuscarPorPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
            {
                return null;
            }

            string placaNormalizada = placa.Trim().ToUpper();
            return veiculos.FirstOrDefault(v => v.Placa == placaNormalizada);
        }

        public IReadOnlyList<IEletrico> ListarEletricos()
        {
            return veiculos.OfType<IEletrico>().ToList().AsReadOnly();
        }

        public decimal CalcularValorTotalAluguel(int dias)
        {
            if (dias <= 0)
            {
                throw new ArgumentException("A quantidade de dias deve ser maior que zero.");
            }

            return veiculos.Sum(v => v.CalcularAluguel(dias));
        }

        public string GerarRelatorio()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[FROTA] {Nome} | Total de veículos: {veiculos.Count}");

            if (veiculos.Count == 0)
            {
                sb.Append("  (Nenhum veículo alocado nesta frota)");
                return sb.ToString();
            }

            foreach (Veiculo veiculo in veiculos)
            {
                sb.AppendLine($"  - {veiculo.GerarRelatorio()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
