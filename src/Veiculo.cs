using System;

namespace UrbanDrive
{
    public class Veiculo
    {
        private int identificador;
        private string placa = string.Empty;
        private string modelo = string.Empty;
        private decimal valorDiaria;

        public int Identificador
        {
            get { return identificador; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("O identificador deve ser maior que zero.");
                }
                identificador = value;
            }
        }

        public string Placa
        {
            get { return placa; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A placa não pode ser nula, vazia ou conter apenas espaços.");
                }
                placa = value.Trim().ToUpper();
            }
        }

        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O modelo não pode ser nulo ou vazio.");
                }
                modelo = value.Trim();
            }
        }

        public decimal ValorDiaria
        {
            get { return valorDiaria; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("O valor da diária deve ser maior que zero.");
                }
                valorDiaria = value;
            }
        }

        public Veiculo(int identificador, string placa, string modelo, decimal valorDiaria)
        {
            Identificador = identificador;
            Placa = placa;
            Modelo = modelo;
            ValorDiaria = valorDiaria;
        }
    }
}
