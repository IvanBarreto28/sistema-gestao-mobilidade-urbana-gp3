using System;

namespace UrbanDrive.Models.Entities
{
    public class Carro : Veiculo
    {
        // Regra de aluguel do Carro: valor da base + seguro fixo por dia
        public const decimal SeguroDiario = 15.00m;

        private int quantidadePortas;

        public int QuantidadePortas
        {
            get { return quantidadePortas; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("A quantidade de portas deve ser maior que zero.");
                }

                quantidadePortas = value;
            }
        }

        public Carro(int identificador, string placa, string modelo,
            decimal valorDiaria, int quantidadePortas)
            : base(identificador, placa, modelo, valorDiaria)
        {
            QuantidadePortas = quantidadePortas;
        }

        public override decimal CalcularAluguel(int dias)
        {
            // A base valida os dias (lança ArgumentException se dias <= 0)
            decimal valorBase = base.CalcularAluguel(dias);

            return valorBase + (SeguroDiario * dias);
        }

        public override string GerarRelatorio()
        {
            return $"[CARRO] ID: {Identificador} | Placa: {Placa} | Modelo: {Modelo} | " +
                   $"Diária: R$ {ValorDiaria:F2} | Portas: {QuantidadePortas}";
        }
    }
}
