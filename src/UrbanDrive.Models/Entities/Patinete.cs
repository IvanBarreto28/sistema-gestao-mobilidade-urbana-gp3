using System;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities
{
    public sealed class Patinete : Veiculo, IEletrico
    {
        // Regra de aluguel do Patinete: 10% de desconto a partir de 3 dias
        public const int DiasParaDesconto = 3;
        public const decimal PercentualDesconto = 0.10m;

        private int autonomiaBateria;
        private int nivelBateria = 100;

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

        public int NivelBateria
        {
            get { return nivelBateria; }
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

        public void Rodar(int km)
        {
            if (km <= 0)
            {
                throw new ArgumentException("A distância percorrida deve ser maior que zero.");
            }

            int consumo = (int)Math.Ceiling(km * 100.0 / AutonomiaBateria);

            if (consumo > nivelBateria)
            {
                throw new InvalidOperationException(
                    $"Bateria insuficiente: percorrer {km} km exige {consumo}% e o nível atual é {nivelBateria}%.");
            }

            nivelBateria -= consumo;
        }

        // Comportamento do patinete: recarga rápida na base, sempre até 100%
        public string Recarregar()
        {
            if (nivelBateria == 100)
            {
                return $"O patinete {Modelo} (Placa: {Placa}) já está com a bateria completa.";
            }

            int recarregado = 100 - nivelBateria;
            nivelBateria = 100;

            return $"Patinete {Modelo} (Placa: {Placa}) recarregado na base: +{recarregado}%. Bateria em 100%.";
        }

        public override string GerarRelatorio()
        {
            return $"[PATINETE] ID: {Identificador} | Placa: {Placa} | Modelo: {Modelo} | " +
                   $"Diária: R$ {ValorDiaria:F2} | Autonomia: {AutonomiaBateria} km | " +
                   $"Bateria: {NivelBateria}%";
        }
    }
}
