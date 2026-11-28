using System;
using UrbanDrive.Models.Interfaces;

namespace UrbanDrive.Models.Entities
{
    /// <summary>
    /// Carro com bateria. Herda de Carro (portas e seguro) e implementa IEletrico.
    /// </summary>
    public class CarroEletrico : Carro, IEletrico
    {
        // Regra de aluguel do CarroEletrico: valor do Carro + taxa de energia por dia
        public const decimal TaxaRecargaDiaria = 10.00m;

        private decimal capacidadeBateriaKwh;
        private int autonomiaBateria;
        private int nivelBateria = 100;

        public decimal CapacidadeBateriaKwh
        {
            get { return capacidadeBateriaKwh; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("A capacidade da bateria deve ser maior que zero.");
                }
                capacidadeBateriaKwh = value;
            }
        }

        // Autonomia, em km, com a bateria 100% carregada
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

        public CarroEletrico(int identificador, string placa, string modelo, decimal valorDiaria,
            int quantidadePortas, decimal capacidadeBateriaKwh, int autonomiaBateria)
            : base(identificador, placa, modelo, valorDiaria, quantidadePortas)
        {
            CapacidadeBateriaKwh = capacidadeBateriaKwh;
            AutonomiaBateria = autonomiaBateria;
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

        // Comportamento do carro elétrico: recarrega em estação e informa a energia em kWh
        public string Recarregar()
        {
            if (nivelBateria == 100)
            {
                return $"O carro elétrico {Modelo} (Placa: {Placa}) já está com a bateria completa.";
            }

            int recarregado = 100 - nivelBateria;
            decimal energiaKwh = CapacidadeBateriaKwh * recarregado / 100m;
            nivelBateria = 100;

            return $"Carro elétrico {Modelo} (Placa: {Placa}) recarregado em estação: " +
                   $"+{recarregado}% ({energiaKwh:F1} kWh). Bateria em 100%.";
        }

        public override decimal CalcularAluguel(int dias)
        {
            // Carro.CalcularAluguel já valida os dias e soma o seguro
            decimal valorCarro = base.CalcularAluguel(dias);

            return valorCarro + (TaxaRecargaDiaria * dias);
        }

        public override string GerarRelatorio()
        {
            return $"[CARRO ELÉTRICO] ID: {Identificador} | Placa: {Placa} | Modelo: {Modelo} | " +
                   $"Diária: R$ {ValorDiaria:F2} | Portas: {QuantidadePortas} | " +
                   $"Capacidade: {CapacidadeBateriaKwh:F1} kWh | Autonomia: {AutonomiaBateria} km | " +
                   $"Bateria: {NivelBateria}%";
        }
    }
}
