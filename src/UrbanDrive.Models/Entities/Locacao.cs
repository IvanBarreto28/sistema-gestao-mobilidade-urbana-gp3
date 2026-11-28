using System;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities
{
    /// <summary>
    /// Representa uma operação de locação associando um veículo e um motorista (RF05).
    /// </summary>
    public class Locacao : IImprimivel
    {
        private int id;
        private int dias;

        public int Id
        {
            get { return id; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("O identificador da locação deve ser maior que zero.");
                }
                id = value;
            }
        }

        public Veiculo Veiculo { get; }
        public Motorista Motorista { get; }
        public DateTime DataLocacao { get; }

        public int Dias
        {
            get { return dias; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("A quantidade de dias da locação deve ser maior que zero.");
                }
                dias = value;
            }
        }

        public decimal TaxaAplicada { get; }
        public decimal ValorBase { get; }
        public decimal ValorTotal { get; }

        public Locacao(int id, Veiculo veiculo, Motorista motorista, int dias)
        {
            Id = id;
            Veiculo = veiculo ?? throw new ArgumentNullException(nameof(veiculo), "O veículo não pode ser nulo.");
            Motorista = motorista ?? throw new ArgumentNullException(nameof(motorista), "O motorista não pode ser nulo.");
            Dias = dias;

            if (Motorista.CarteiraHabilitacao.DataValidade.Date < DateTime.Today)
            {
                throw new InvalidOperationException("Não é possível realizar locação com CNH vencida.");
            }

            DataLocacao = DateTime.Now;
            TaxaAplicada = ConfiguradorSistema.TaxaLocacao;
            ValorBase = Veiculo.CalcularAluguel(Dias);
            ValorTotal = ConfiguradorSistema.AplicarTaxaLocacao(ValorBase);
        }

        public string GerarRelatorio()
        {
            return $"[LOCAÇÃO #{Id}] Motorista: {Motorista.Nome} (CNH: {Motorista.CarteiraHabilitacao.NumeroRegistro}) | " +
                   $"Veículo: {Veiculo.Modelo} ({Veiculo.Placa}) | Dias: {Dias} | " +
                   $"Base: R$ {ValorBase:F2} | Taxa: {TaxaAplicada * 100:F0}% | Total: R$ {ValorTotal:F2}";
        }
    }
}
