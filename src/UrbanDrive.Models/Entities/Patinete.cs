using System;

namespace UrbanDrive.Models.Entities
{
    public sealed class Patinete : Veiculo
    {
        private int autonomiaBateria;

        public int AutonomiaBateria
        {
            get { return autonomiaBateria; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("A autonomia da bateria deve ser maior que zero.");
                }
                autonomiaBateria = value;
            }
        }

        public Patinete(int identificador, string placa, string modelo, decimal valorDiaria, int autonomiaBateria)
            : base(identificador, placa, modelo, valorDiaria)
        {
            AutonomiaBateria = autonomiaBateria;
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