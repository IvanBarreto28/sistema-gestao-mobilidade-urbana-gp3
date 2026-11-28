namespace UrbanDrive.Models.Interfaces
{
    /// <summary>
    /// Contrato para veículos elétricos (RF09).
    /// Cada tipo de veículo elétrico decide como se comporta ao rodar e ao recarregar.
    /// </summary>
    public interface IEletrico
    {
        /// <summary>
        /// Nível atual da bateria, de 0 a 100 (%).
        /// </summary>
        int NivelBateria { get; }

        /// <summary>
        /// Consome a bateria de acordo com a distância percorrida.
        /// Lança ArgumentException se km for zero ou negativo e
        /// InvalidOperationException se a bateria não for suficiente.
        /// </summary>
        void Rodar(int km);

        /// <summary>
        /// Recarrega a bateria e devolve um texto descrevendo a operação.
        /// </summary>
        string Recarregar();
    }
}
