
using System;

namespace UrbanDrive.Models.Entities
{
    public class CNH
    {
        private string numeroRegistro = string.Empty;
        private string categoria = string.Empty;
        private DateTime dataValidade;

        public string NumeroRegistro
        {
            get { return numeroRegistro; }
        }

        public string Categoria
        {
            get { return categoria; }
        }

        public DateTime DataValidade
        {
            get { return dataValidade; }
        }

        internal CNH(string numeroRegistro, string categoria, DateTime dataValidade)
        {
            if (string.IsNullOrWhiteSpace(numeroRegistro))
            {
                throw new ArgumentException("O número de registro não pode ser vazio.");
            }

            if (string.IsNullOrWhiteSpace(categoria))
            {
                throw new ArgumentException("A categoria não pode ser vazia.");
            }

            this.numeroRegistro = numeroRegistro;
            this.categoria = categoria;
            this.dataValidade = dataValidade;
        }
    }
}
