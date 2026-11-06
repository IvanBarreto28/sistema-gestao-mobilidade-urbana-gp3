
using System;

namespace UrbanDrive.Models.Entities
{
    public class Motorista
    {
        private string nome = string.Empty;
        private string cpf = string.Empty;

        public string Nome
        {
            get { return nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O nome não pode ser vazio.");
                }

                nome = value;
            }
        }

        public string CPF
        {
            get { return cpf; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O CPF não pode ser vazio.");
                }

                cpf = value;
            }
        }

        public CNH CarteiraHabilitacao { get; private set; }

        public Motorista(string nome, string cpf, string numeroRegistro,
            string categoria, DateTime dataValidade)
        {
            Nome = nome;
            CPF = cpf;

            CarteiraHabilitacao = new CNH(
                numeroRegistro,
                categoria,
                dataValidade
            );
        }
    }
}
