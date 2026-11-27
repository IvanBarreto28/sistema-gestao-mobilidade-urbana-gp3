using System;

namespace UrbanDrive.Models.Entities
{
    public class Carro : Veiculo
    {
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
            if (dias <= 0)
            {
                throw new ArgumentException("A quantidade de dias deve ser maior que zero.");
            }

            return ValorDiaria * dias;
        }
    }
}