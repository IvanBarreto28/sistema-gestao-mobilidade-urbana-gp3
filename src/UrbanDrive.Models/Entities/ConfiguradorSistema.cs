using System;

namespace UrbanDrive.Models.Entities
{

    public static class ConfiguradorSistema
    {

        public const decimal TaxaLocacaoPadrao = 0.10m;

        private static decimal taxaLocacao = TaxaLocacaoPadrao;
        private static int totalVeiculos = 0;

        public static decimal TaxaLocacao
        {
            get { return taxaLocacao; }
            set
            {
                if (value < 0m || value > 1m)
                {
                    throw new ArgumentException("A taxa de locação deve estar entre 0 e 1 (ex.: 0,10 para 10%).");
                }
                taxaLocacao = value;
            }
        }

        public static int TotalVeiculos
        {
            get { return totalVeiculos; }
        }

        internal static void RegistrarVeiculo()
        {
            totalVeiculos++;
        }

        public static decimal AplicarTaxaLocacao(decimal valorBase)
        {
            if (valorBase <= 0m)
            {
                throw new ArgumentException("O valor-base deve ser maior que zero.");
            }
            return valorBase * (1m + taxaLocacao);
        }

        public static void RestaurarTaxaPadrao()
        {
            taxaLocacao = TaxaLocacaoPadrao;
        }
    }
}
