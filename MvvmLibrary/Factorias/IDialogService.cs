using System.Threading.Tasks;

namespace MvvmLibrary.Factorias
{
    public interface IDialogService
    {
        Task MostrarAlerta(string titulo, string msg, string cancelar);
        Task<bool> MostrarAlerta(string titulo, string msg, string aceptar, string cancelar);
        Task<string> MostrarActionSheet(string titulo, string cancelar, string destruccion, params string[] botones);
    }
}