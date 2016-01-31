using Xamarin.Forms;

namespace MvvmLibrary.Factorias
{
    public interface IPage : IDialogService
    {
        INavigation Navigation { get; }
    }
}