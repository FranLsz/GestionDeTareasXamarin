using System;
using System.Threading.Tasks;
using MvvmLibrary.ViewModel.Base;

namespace MvvmLibrary.Factorias
{
    // Añade o quita ventanas 
    public interface INavigator
    {
        // Elimina la ventana actual (volver atrás)
        Task<IViewModel> PopAsync();

        // Elimina la ventana principal
        Task<IViewModel> PopModalAsync();

        // Elimina todas las ventanas hasta la principal
        Task PopToRootAsync();

        // Añade una nueva ventana apilable sobre la actual
        Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> action = null)
            where TViewModel : class, IViewModel;
        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;

        // Añade una ventana modal 
        Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> action = null)
            where TViewModel : class, IViewModel;
        Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;
    }
}