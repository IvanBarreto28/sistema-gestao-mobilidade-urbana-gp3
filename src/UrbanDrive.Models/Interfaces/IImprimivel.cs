namespace UrbanDrive.Models.Interfaces
{
    /// <summary>
    /// Define o contrato para objetos que podem gerar um relatório textual.
    /// </summary>
    public interface IImprimivel
    {
        string GerarRelatorio();
    }
}
