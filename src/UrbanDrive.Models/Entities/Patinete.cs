using System;

namespace UrbanDrive.Models.Entities
{
    public sealed class Patinete : Veiculo
    {
        // Regra de aluguel do Patinete: 10% de desconto a partir de 3 dias
        public const int DiasParaDesconto = 3;
        public const decimal PercentualDesconto = 0.10m;

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
            // A base valida os dias (lança ArgumentException se dias <= 0)
            decimal valorBase = base.CalcularAluguel(dias);

            if (dias >= DiasParaDesconto)
            {
                return valorBase * (1m - PercentualDesconto);
            }

            return valorBase;
        }

        public override string GerarRelatorio()
        {
            return $"[PATINETE] ID: {Identificador} | Placa: {Placa} | Modelo: {Modelo} | " +
                   $"Diária: R$ {ValorDiaria:F2} | Autonomia: {AutonomiaBateria} km";
        }
    }
}
